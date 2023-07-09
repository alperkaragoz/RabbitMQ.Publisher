using RabbitMQ.Client;
using System.Text;

// Bağlantıyı oluşturuyoruz.
ConnectionFactory factory = new();
factory.Uri = new("amqps://gagxjjis:g1rs6slF9bx-4tKOXHlP-NVypI8LlG8u@toad.rmq.cloudamqp.com/gagxjjis");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string exchangeName = "pub-sub-exchange-exam";

channel.ExchangeDeclare(
    exchange: exchangeName,
    type: ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes("Hello " + i);

    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: string.Empty,
        body: message
        );
}

Console.Read();