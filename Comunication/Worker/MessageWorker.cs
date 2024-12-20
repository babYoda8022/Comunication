
using RabbitMQ.Iot;
using RabbitMQ.Services.Interface;

namespace Comunication.Worker
{
    public class MessageWorker : BackgroundService
    {
        private readonly ILogger<MessageWorker> _logger;
        private readonly IMessage _messageService;
        public MessageWorker(ILogger<MessageWorker> logger, IMessage messageService) 
        {
            _logger = logger;
            _messageService = messageService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             await _messageService.ConsumerAsync(Queue.FILA_TESTE, x =>
             {
                 _logger.LogInformation("Mensagem Recebida");
             });
        }
    }
}
