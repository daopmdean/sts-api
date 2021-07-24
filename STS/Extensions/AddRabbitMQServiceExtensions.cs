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
                VirtualHost = configuration
                    .GetSection("RabbitMqHeroku")["VirtualHost"],
                HostName = configuration
                    .GetSection("RabbitMqHeroku")["HostName"],
                UserName = configuration
                    .GetSection("RabbitMqHeroku")["UserName"],
                Password = configuration
                    .GetSection("RabbitMqHeroku")["Password"]
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "sts_api_request",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            services.AddSingleton(channel);

            return services;
        }
    }
}
