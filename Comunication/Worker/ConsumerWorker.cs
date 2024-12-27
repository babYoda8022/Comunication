
using RabbitMQ.Iot;
using RabbitMQ.Services.Interface;

namespace Comunication.Worker
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly ILogger<ConsumerWorker> _logger;
        private readonly IMessageService _messageService;
        public ConsumerWorker(ILogger<ConsumerWorker> logger, IMessageService messageService) 
        {
            _logger = logger;
            _messageService = messageService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageService.ConsumerAsync<string>(Queue.TEST, x =>
            {
                _logger.LogInformation($"Message received : {x.Body}");
            });
        }
    }
}
