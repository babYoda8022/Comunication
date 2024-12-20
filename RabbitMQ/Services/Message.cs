using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Iot;
using RabbitMQ.Services.Interface;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Services
{
    public class Message : Base, IMessage
    {
        public Message(IOptions<ConfigSettings> config) : base(config)
        {
        }

        public async Task SendAsync(string message, string queue)
        {
            await InitializeAsync();
            await CreateQueue(queue);

            byte[] body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(
                exchange: "",
                routingKey: queue,
                mandatory: false,
                body: body,
                cancellationToken: CancellationToken.None
            );
        }
        public async Task ConsumerAsync(string queue, Action<string> onMessageReceived)
        {
            await InitializeAsync();
            await CreateQueue(queue);

            var consummer = new AsyncEventingBasicConsumer(_channel);

            consummer.ReceivedAsync += async (model, args) =>
            {
                byte[] body = args.Body.ToArray();
                string resp = Encoding.UTF8.GetString(body);
                onMessageReceived(resp);
            };

            await _channel.BasicConsumeAsync(queue, true, consummer);
        }
        public async Task SendAsync<T>(T message, string queue)
        {
            await InitializeAsync();
            await CreateQueue(queue);

            byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await _channel.BasicPublishAsync(
                exchange: "",
                routingKey: queue,
                mandatory: false,
                body: body,
                cancellationToken: CancellationToken.None
            );
        }
        public async Task ConsumerAsync<T>(string queue, Action<T> onMessageReceived) where T : new()
        {
            await InitializeAsync();
            await CreateQueue(queue);

            var consummer = new AsyncEventingBasicConsumer(_channel);
            T resp = new T();

            consummer.ReceivedAsync += async (model, args) =>
            {
                byte[] body = args.Body.ToArray();
                resp = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));
                onMessageReceived(resp);
            };
            
            await _channel.BasicConsumeAsync(queue, true, consummer);
        }
       
    }
}
