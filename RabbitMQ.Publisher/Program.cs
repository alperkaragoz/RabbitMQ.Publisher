using RabbitMQ.Client;
using System.Text;

// Bağlantıyı oluşturuyoruz.
ConnectionFactory factory = new();
factory.Uri = new("amqps://gagxjjis:g1rs6slF9bx-4tKOXHlP-NVypI8LlG8u@toad.rmq.cloudamqp.com/gagxjjis");

// Bağlantıyı aktifleştiriyoruz ve kanal açıyoruz.IConnection bir IDisposable olduğu için using keywordü ile kullanıyoruz ki bu işlem tamamlandıktan sonra bu nesne dispose edilip bellekte gerekli allocation'lar temizlenmiş olsun.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    //$"{}" > string interpolation
    byte[] message = Encoding.UTF8.GetBytes($" Hello {i}");

    channel.BasicPublish(
        exchange: "fanout-exchange-example",
        routingKey: string.Empty,
        body: message);
}

Console.Read();