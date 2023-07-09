using RabbitMQ.Client;
using System.Text;

// Bağlantıyı oluşturuyoruz.
ConnectionFactory factory = new();
factory.Uri = new("amqps://gagxjjis:g1rs6slF9bx-4tKOXHlP-NVypI8LlG8u@toad.rmq.cloudamqp.com/gagxjjis");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//string queueName = "p2p-queue-example";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//byte[] message = Encoding.UTF8.GetBytes("Hello");

//channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: queueName,
//        body: message);

Console.Read();