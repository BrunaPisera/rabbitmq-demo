using RabbitMQ.Client.Events;
using System.Text;

namespace ConsumerApp.Infra.Messaging
{
    public class RabbitMqConsumer
    {
        private readonly RabbitMqConnection _connection;
        private readonly string _queueName;

        public RabbitMqConsumer(RabbitMqConnection connection, string queueName)
        {
            _connection = connection;
            _queueName = queueName;
        }

        public void Start(CancellationToken cancellationToken)
        {
            _connection.DeclareQueue(_queueName);

            var consumer = new EventingBasicConsumer(_connection.Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Received: {message}");
            };

            _connection.Channel.BasicConsume(
                                        queue: _queueName,
                                        autoAck: true,
                                        consumerTag: "",
                                        noLocal: false,
                                        exclusive: false,
                                        arguments: null,
                                        consumer: consumer
                                    );


            Console.WriteLine("Consumer started...");

            while (!cancellationToken.IsCancellationRequested)
                Thread.Sleep(1000);
        }
    }
}
