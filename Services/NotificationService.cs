using Microsoft.Extensions.Configuration;
using Models;
using Models.DTO;
using RabbitMQ.Client;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationService : INotificationService, IDisposable
    {
        private readonly IConnection _connection;

        public NotificationService(IConfiguration configuration)
        {
            var rabbitMqConfig = configuration.GetSection("RabbitMQ");

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqConfig["HostName"],
                Port = int.Parse(rabbitMqConfig["Port"]),
                UserName = rabbitMqConfig["UserName"],
                Password = rabbitMqConfig["Password"],
                VirtualHost = rabbitMqConfig["VirtualHost"]
            };

            _connection = factory.CreateConnection();
        }

        public async Task SendAsync(string message)
        {
            MessageDto messageDto = new MessageDto
            {
                Content = message
            };

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MessageQueue",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: "MessageQueue",
                               basicProperties: null,
                               body: body);
            }

            // здесь будет брокер.
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }
}
