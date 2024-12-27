using Comunication.Worker;
using RabbitMQ.Iot;
using RabbitMQ.Services;
using RabbitMQ.Services.Interface;

namespace Comunication.Extensions
{
    public static class ConfigServices
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddSingleton<IMessageService, MessageService>();
            service.AddBackgroundServices();

            return service;
        }

        public static IServiceCollection AddBackgroundServices(this IServiceCollection service)
        {
            service.AddHostedService<ConsumerWorker>();
            service.AddHostedService<SendMessageWorker>();
            return service;
        }

        public static HostApplicationBuilder AddConfigSettings(this HostApplicationBuilder builder)
        {
            builder.Services.Configure<ConfigSettings>(builder.Configuration.GetSection("RabbitMQ"));
            return builder;
        }
    }
}
