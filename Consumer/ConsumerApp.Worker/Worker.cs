using ConsumerApp.Infra.Messaging;

namespace ConsumerService
{
    public class Worker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rabbitConnection = new RabbitMqConnection("rabbitmq", "", "");

            var consumer = new RabbitMqConsumer(rabbitConnection);

            consumer.StartConsumer("demo-queue");

            return Task.CompletedTask;
        }
    }
}
