using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Iot;
using RabbitMQ.Model;
using RabbitMQ.Services.Interface;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Services
{
    public class MessageService : Base, IMessageService
    {
        public MessageService(IOptions<ConfigSettings> config) : base(config)
        {
        }

        public async Task SendAsync<T>(SendMessageModel<T> sendMessage)
        {
            await InitializeAsync();
            await CreateQueue(sendMessage.RoutingKey);

            byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sendMessage));

            await _channel.BasicPublishAsync(
                exchange: sendMessage.Exchange,
                routingKey: sendMessage.RoutingKey,
                mandatory: sendMessage.Mandatory,
                body: body,
                cancellationToken: sendMessage.CancellationToken
            );
        }
        public async Task ConsumerAsync<T>(string queue, Action<ConsumerModel<T>> onMessageReceived)
        {
            await InitializeAsync();
            await CreateQueue(queue);

            ConsumerModel<T> resp = new(); 

            var consummer = new AsyncEventingBasicConsumer(_channel);

            consummer.ReceivedAsync += async (model, args) =>
            {
                byte[] body = args.Body.ToArray();
                resp = JsonSerializer.Deserialize<ConsumerModel<T>>(Encoding.UTF8.GetString(body));
                onMessageReceived(resp);
            };
            
            await _channel.BasicConsumeAsync(queue, true, consummer);
        }
    }
}
