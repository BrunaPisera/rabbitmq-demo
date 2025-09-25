using RabbitMQ.Client;

namespace ConsumerApp.Infra.Messaging
{
    public class RabbitMqConnection
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqConnection(string hostName, string userName, string password)
        {
            _factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        public IConnection CreateConnection()
        {
            return _factory.CreateConnection();
        }       
    }
}
