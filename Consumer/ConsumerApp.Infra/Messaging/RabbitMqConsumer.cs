using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ConsumerApp.Infra.Messaging
{
    public class RabbitMqConsumer
    {
        private readonly RabbitMqConnection _connection;

        public RabbitMqConsumer(RabbitMqConnection connection)
        {
            _connection = connection;
        }

        public void StartConsumer(string queueName)
        {
            var connection = _connection.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"[x] Received: {message}");
            };

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine("Consumer started...");
        }
    }
}
