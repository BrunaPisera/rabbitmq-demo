using Microsoft.AspNetCore.Mvc;
using ProducerApp.Infra.Messaging;

namespace ProducerApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly RabbitMqPublisher _publisher;

        public MessagesController(RabbitMqPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            _publisher.Publish(message);
            return Ok(new { Status = "Mensagem enviada!" });
        }
    }
}
