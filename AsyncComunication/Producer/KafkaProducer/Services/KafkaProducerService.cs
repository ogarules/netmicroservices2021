using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace KafkaProducer.Services
{
    /// <summary>
    /// Clase generica para encolar mensajes en un topic de kafka
    /// </summary>
    /// <typeparam name="TKey">Tipo de dato de la llave del mensaje</typeparam>
    /// <typeparam name="TValue">Tipo de dato del valor del mensaje</typeparam>
    public class KafkaProducerService<TKey, TValue> : IDisposable where TValue : class
    {
        /// <summary>
        /// Producer de kafka
        /// </summary>
        private readonly IProducer<TKey, string> producer;

        /// <summary>
        /// Topic de kafka
        /// </summary>
        private readonly string topic;

        /// <summary>
        /// Crea una nueva instancia del servicio encolador de kafka
        /// </summary>
        /// <param name="producerConfig">Configuracion de kafka</param>
        /// <param name="configuration">Configuraciond de la aplicacion</param>
        public KafkaProducerService(ProducerConfig producerConfig, IConfiguration configuration)
        {
            // Inicializamos la conectividad
            this.producer = new ProducerBuilder<TKey, string>(producerConfig).Build();
            this.topic = configuration.GetValue<string>("topic");
        }

        /// <summary>
        /// Encolamos un nuevo mensaje en el topic de kafka
        /// </summary>
        /// <param name="key">Llave de mensaje</param>
        /// <param name="value">Valor del mensaje</param>
        /// <returns>Contexto de sincronizacion</returns>
        public async Task ProduceMessage(TKey key, TValue value)
        {
            var result = await this.producer.ProduceAsync(this.topic, new Message<TKey, string>
            {
                Key = key,
                Value = Newtonsoft.Json.JsonConvert.SerializeObject(value)
            });           
        }

        /// <summary>
        /// inberacion de los recursos de kafka
        /// </summary>
        public void Dispose()
        {
            // Enviamos los mensajes pendientes de enviar y liberamos la conectividad hacia kafka
            this.producer.Flush();
            this.producer.Dispose();
        }
    }
}