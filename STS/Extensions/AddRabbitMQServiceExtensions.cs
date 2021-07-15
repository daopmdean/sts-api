using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace STS.Extensions
{
    public static class AddRabbitMQServiceExtensions
    {
        public static IServiceCollection AddRabbitMQService(
            this IServiceCollection services, IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration.GetSection("RabbitMq")["HostName"],
                UserName = configuration.GetSection("RabbitMq")["UserName"],
                Password = configuration.GetSection("RabbitMq")["Password"]
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "sts_api_request",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            //var properties = channel.CreateBasicProperties();
            //properties.Persistent = true;
            services.AddSingleton(channel);

            return services;
        }
    }
}
