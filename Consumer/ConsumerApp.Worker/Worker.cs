using ConsumerApp.Infra.Messaging;

namespace ConsumerService
{
    public class Worker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rabbitConnection = new RabbitMqConnection("rabbitmq", "", "");
            var consumer = new RabbitMqConsumer(rabbitConnection, "demo-queue");

            Task.Run(() => consumer.Start(stoppingToken), stoppingToken);

            return Task.CompletedTask;
        }
    }
}
