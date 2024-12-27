
using RabbitMQ.Iot;
using RabbitMQ.Model;
using RabbitMQ.Services.Interface;

namespace Comunication.Worker
{
    public class SendMessageWorker : BackgroundService
    {
        private readonly ILogger<SendMessageWorker> _logger;
        private readonly IMessageService _messageService;
        public SendMessageWorker(ILogger<SendMessageWorker> logger, IMessageService messageService) 
        {
            _logger = logger;
            _messageService = messageService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SendMessageModel<string> message = new()
            {
                RoutingKey = Queue.TEST,
                Body = "Sending a test message"
            };

            while (true)
            {
                Thread.Sleep(3000);
                await _messageService.SendAsync<string>(message);
                _logger.LogInformation($"Message sending : {message.Body}");
            }
        }
    }
}
