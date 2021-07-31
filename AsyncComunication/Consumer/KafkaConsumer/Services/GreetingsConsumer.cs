using System.Threading.Tasks;
using Confluent.Kafka;
using KafkaConsumer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KafkaConsumer.Services
{
    public class GreetingsConsumer : KafkaConsumerBase<int, Greeting>
    {
        ILogger<GreetingsConsumer> logger;

        public GreetingsConsumer(ILogger<GreetingsConsumer> logger, ConsumerConfig consumerConfig, IConfiguration configuration)
        : base(consumerConfig, configuration)
        {
            this.logger = logger;
        }

        public override async Task Consume(int key, Greeting value)
        {
            this.logger.LogInformation($"Greeting id => {key}");
            this.logger.LogInformation($"Greeting message => {value.Name}");
            
        }
    }
}