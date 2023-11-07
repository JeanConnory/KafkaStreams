

#region RabbitMQ

//using System.Text;
//using System.Text.Json;
//using Rabbit.Models.Entities;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;

//var factory = new ConnectionFactory
//{
//    HostName = "localhost",
//    UserName = "guest",
//    Password = "guest"
//};
//using var connection = factory.CreateConnection();

//using var channel = connection.CreateModel();

//channel.QueueDeclare(queue: "rabbitMensagensQueue",
//                     durable: false,
//                     exclusive: false,
//                     autoDelete: false,
//                     arguments: null);

//var consumer = new EventingBasicConsumer(channel);

//consumer.Received += (model, ea) =>
//{
//    var body = ea.Body.ToArray();
//    var json = Encoding.UTF8.GetString(body);
//    AppMensagem mensagem = JsonSerializer.Deserialize<AppMensagem>(json);

//    Thread.Sleep(1000);

//    Console.WriteLine($"Mensagem consumida\n Titulo: {mensagem.Titulo}, Texto: {mensagem.Texto}, Id: {mensagem.Id}");
//};

//channel.BasicConsume(queue: "rabbitMensagensQueue",
//                     autoAck: true,
//                     consumer: consumer);

//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();

#endregion

#region Kafka

using Confluent.Kafka;
using Rabbit.Models.Entities;
using System.Text.Json;

CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = $"queue_kafka-group-0",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<string, string>(config).Build())
{
    consumer.Subscribe("queue_kafka");
    while (!cts.IsCancellationRequested)
    {
        try
        {
            var cr = consumer.Consume(cts.Token);
            var json = cr.Message.Value;
            AppMensagem mensagem = JsonSerializer.Deserialize<AppMensagem>(json);
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"Titulo: {mensagem.Titulo}; Texto={mensagem.Texto}; Id={mensagem.Id}");
        }
        catch (OperationCanceledException oce)
        {
            continue;
        }
    }
}

#endregion