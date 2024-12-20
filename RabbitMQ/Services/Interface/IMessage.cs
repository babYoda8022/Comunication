namespace RabbitMQ.Services.Interface
{
    public interface IMessage
    {
        public Task SendAsync(string message, string queue);
        public Task SendAsync<T>(T message, string queue);
        public Task ConsumerAsync(string queue, Action<string> onMessageReceived);
        public Task ConsumerAsync<T>( string queue, Action<T> onMessageReceived) where T : new();
    }
}
