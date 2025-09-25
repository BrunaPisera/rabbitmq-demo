using Microsoft.AspNetCore.Mvc;
using ProducerApp.Infra.Messaging;

namespace ProducerApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly RabbitMqPublisher _publisher;

        public MessagesController()
        {
            var connection = new RabbitMqConnection("rabbitmq", "", "");
            _publisher = new RabbitMqPublisher(connection);
        }

        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            _publisher.Publish("demo-queue", message);
            return Ok(new { Status = "Mensagem enviada!" });
        }
    }
}
