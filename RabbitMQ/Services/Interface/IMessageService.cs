using RabbitMQ.Model;

namespace RabbitMQ.Services.Interface
{
    public interface IMessageService
    {
        public Task SendAsync<T>(SendMessageModel<T> sendMessage);
        public Task ConsumerAsync<T>(string queue, Action<ConsumerModel<T>> onMessageReceived);
    }
}
