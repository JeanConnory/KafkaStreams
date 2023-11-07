using Confluent.Kafka;
using Rabbit.Models.Entities;
using Rabbit.Repositories.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Rabbit.Repositories
{
    public class KafkaMensagemRepository : IAppMensagemRepository
    {
        public void SendMensagem(AppMensagem mensagem)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                string json = JsonSerializer.Serialize(mensagem);
                producer.Produce("queue_kafka",
                                        new Message<string, string>
                                        {
                                            Key = Guid.NewGuid().ToString(),
                                            Value = json
                                        });
            }
        }
    }
}
