using RabbitMQ.Client;
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

            channel.ExchangeDeclare(
                exchange: "meu-exchange-fanout",
                type: ExchangeType.Fanout,
                durable: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare(
                queue: "demo-queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind(
                queue: "demo-queue",
                exchange: "meu-exchange-fanout",
                routingKey: "");

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "meu-exchange-fanout",
                routingKey: "",
                basicProperties: null,
                body: body);
        }
    }
}
