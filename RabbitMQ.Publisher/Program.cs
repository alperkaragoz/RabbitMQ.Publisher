using RabbitMQ.Client;
using System.Text;

// Bağlantıyı oluşturuyoruz.
ConnectionFactory factory = new();
factory.Uri = new("amqps://gagxjjis:g1rs6slF9bx-4tKOXHlP-NVypI8LlG8u@toad.rmq.cloudamqp.com/gagxjjis");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers
    );

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($" Hello {i}"); 
    Console.WriteLine("Please set header value: ");
    string value = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string, object>()
    {
        ["no"] = value
    };


    //basicProperties amacı, publish sürecinde "header-exchange-example" 'ın headerına bir değer vermek için kullanıyoruz.
    channel.BasicPublish(
        exchange: "header-exchange-example",
        routingKey: string.Empty,
        body: message,
        basicProperties: basicProperties
        );
}

Console.Read();