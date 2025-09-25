﻿using RabbitMQ.Client;
using System.Text;

namespace ProducerApp.Infra.Messaging
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqConnection _connection;

        public RabbitMqPublisher(RabbitMqConnection connection)
        {
            _connection = connection;
        }

        public void Publish(string queueName, string message)
        {
            using var connection = _connection.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: body);
        }
    }
}
