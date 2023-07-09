using RabbitMQ.Client;
using System.Text;

// Bağlantıyı oluşturuyoruz.
ConnectionFactory factory = new();
factory.Uri = new("amqps://gagxjjis:g1rs6slF9bx-4tKOXHlP-NVypI8LlG8u@toad.rmq.cloudamqp.com/gagxjjis");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "work-queue-example";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

IBasicProperties basicProperties = channel.CreateBasicProperties();
basicProperties.Persistent = true;

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] body = Encoding.UTF8.GetBytes("Hello " + i);
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: queueName,
        body: body,
        basicProperties: basicProperties);

}

Console.Read();