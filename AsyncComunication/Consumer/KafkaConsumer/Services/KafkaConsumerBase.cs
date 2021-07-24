using System.Reflection.PortableExecutable;
using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace KafkaConsumer.Services
{
    /// <summary>
    /// Claes base para la implementacion de consumidores de kafka
    /// </summary>
    /// <typeparam name="TKey">Tipo de dato de la llave del mensaje</typeparam>
    /// <typeparam name="TValue">Tipo de dato del valor del mensaje</typeparam>
    public abstract class KafkaConsumerBase<TKey, TValue> : IHostedService, IDisposable where TValue : class
    {
        /// <summary>
        /// Configuracion de kafka
        /// </summary>
        private readonly ConsumerConfig consumerConfig;

        /// <summary>
        /// Consumidor de kafka
        /// </summary>
        private IConsumer<TKey, string> consumer;

        /// <summary>
        /// Token default de cancelacion
        /// </summary>
        private CancellationToken cancellationToken1;

        /// <summary>
        /// Topico d ekafka
        /// </summary>
        private string topic;

        /// <summary>
        /// Crea una nueva implementacion del consumidor generico de kafka
        /// </summary>
        /// <param name="consumerConfig">Cnfiguracion de kafka</param>
        /// <param name="configuration">Configuracion de la aplicacion</param>
        public KafkaConsumerBase(ConsumerConfig consumerConfig, IConfiguration configuration)
        {
            this.cancellationToken1 = CancellationToken.None;
            this.consumerConfig = consumerConfig;
            this.topic = configuration.GetValue<string>("topic");
        }

        /// <summary>
        /// Prendido del hosted service dentro de la aplicacion
        /// </summary>
        /// <param name="cancellationToken">Token de cancelacion</param>
        /// <returns>Contexto de sincronizacion</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Inicializamos el consumer de kafka
            this.consumer = new ConsumerBuilder<TKey, string>(this.consumerConfig).Build();

            // Levantamos en un hilo separado le proceso de escucha de mensajes de kafka
            Task.Factory.StartNew(() => this.ExecuteConsumerLoop(cancellationToken), TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Ejecuta la escucha de mensajes de kafka
        /// </summary>
        /// <param name="cancellationToken">Token de cancelacion</param>
        /// <returns>Contexto de sincronizacion</returns>
        private async Task ExecuteConsumerLoop(CancellationToken cancellationToken)
        {
            // nos suscribimos a la cola configurada
            consumer.Subscribe(this.topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try 
                {
                    // Esperamos hasta leer un nuevo mensaje que este disponible en la cola
                    var result = this.consumer.Consume(cancellationToken);

                    // Si el mensaje no es nulo ejecutamos la implementacion final
                    if (result != null)
                    { 
                        await this.Consume(result.Message.Key, Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(result.Message.Value));
                    }
                }
                catch (OperationCanceledException canceledException)
                {
                    Console.WriteLine("Se cancelo la ejecucion");
                    break;
                }
                catch (ConsumeException consumeException)
                {
                    Console.WriteLine("Se trono kafka =>" + consumeException.ToString());
                    if (consumeException.Error.IsFatal)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// Implemenacion final del consumo de la cola
        /// </summary>
        /// <param name="key">Llave del mensaje</param>
        /// <param name="value">Valor del mensaje</param>
        /// <returns>Contexto de sincronizacion</returns>
        public abstract Task Consume(TKey key, TValue value);
 
        /// <summary>
        /// Detiene el consumo de mensajes de kafka
        /// </summary>
        /// <param name="cancellationToken">Token de cancelacion</param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.Dispose();
        }

        /// <summary>
        /// Libera la conectividad <see langword="abstract"/> kafka
        /// </summary>
        public void Dispose()
        {
            this.consumer.Close();
            this.consumer.Dispose();
        }
    }
}