using RabbitMQ.Client;
using System.Text;

// Bağlantıyı oluşturuyoruz.
ConnectionFactory factory = new();
factory.Uri = new("amqps://gagxjjis:g1rs6slF9bx-4tKOXHlP-NVypI8LlG8u@toad.rmq.cloudamqp.com/gagxjjis");

// Bağlantıyı aktifleştiriyoruz ve kanal açıyoruz.IConnection bir IDisposable olduğu için using keywordü ile kullanıyoruz ki bu işlem tamamlandıktan sonra bu nesne dispose edilip bellekte gerekli allocation'lar temizlenmiş olsun.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare("direct-exchange-example", type: ExchangeType.Direct);



while (true)
{
    Console.Write("Message : ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "direct-exchange-example",
        routingKey: "direct-queue-example",
        body: byteMessage);
}

Console.Read();