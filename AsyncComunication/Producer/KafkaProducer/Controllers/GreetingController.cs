using System.Threading.Tasks;
using KafkaProducer.Models;
using KafkaProducer.Services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreetingController : ControllerBase
    {
        private readonly KafkaProducerService<int, Greeting> producer;
        public GreetingController(KafkaProducerService<int, Greeting> producer)
        {
            this.producer = producer;
        }

        [HttpPost]
        public async Task<Greeting> SaveGreeting(Greeting greeting)
        {
            
            await this.producer.ProduceMessage(greeting.Id, greeting);
            
            

            return greeting;
        }
    }
}