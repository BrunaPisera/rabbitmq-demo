using RabbitMQ.Client;

namespace ConsumerApp.Infra.Messaging
{
    public class RabbitMqConnection : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public IModel Channel => _channel;

        public RabbitMqConnection(string hostName, string userName, string pass)
        {
            var factory = new ConnectionFactory() { HostName = hostName, UserName = userName, Password = pass};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void DeclareQueue(string queueName)
        {
            _channel.QueueDeclare(queue: queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }

    }
}
