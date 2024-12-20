using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Iot;

namespace RabbitMQ.Services
{
    public abstract class Base : IDisposable
    {
        private readonly Task<IConnection> _connection;
        protected IChannel _channel { get; private set; }


        protected Base(IOptions<ConfigSettings> config)
        {
            var _config = config.Value;

            ConnectionFactory factory = new()
            {
                HostName = _config.HostName,
                UserName = _config.UserName,
                Password = _config.Password,
                Port = _config.Port
            };

            _connection = factory.CreateConnectionAsync();
        }

        public async Task InitializeAsync()
        {
            IConnection connection = await _connection.ConfigureAwait(false);
            _channel = await connection.CreateChannelAsync();
        }

        public async Task<QueueDeclareOk> CreateQueue(string name)
        {
            return await _channel.QueueDeclareAsync(name, durable: true, exclusive: false, autoDelete: false);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
