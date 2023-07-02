using RabbitMQ.Client;
using System.Text;

// Bağlantıyı oluşturuyoruz.
ConnectionFactory factory = new();
factory.Uri = new("amqps://gagxjjis:g1rs6slF9bx-4tKOXHlP-NVypI8LlG8u@toad.rmq.cloudamqp.com/gagxjjis");

// Bağlantıyı aktifleştiriyoruz ve kanal açıyoruz.IConnection bir IDisposable olduğu için using keywordü ile kullanıyoruz ki bu işlem tamamlandıktan sonra bu nesne dispose edilip bellekte gerekli allocation'lar temizlenmiş olsun.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue oluşturuyoruz. Eğer bir kuyruk exclusive olarak tasarlanıyorsa, o kuyruk o bağlantıya özel oluşturulur, daha sonra da silinir.
channel.QueueDeclare(queue: "example-queue", exclusive: false);

// Queue'ya mesaj gönderiyoruz.RabbitMQ kuyruğa atacağı mesajları byte türünden kabul etmektedir.Mesajları byte'a dönüştürmemiz gerekiyor.
//byte[] message = Encoding.UTF8.GetBytes("Hello");
//// Exchange boş geçildiği takdirde default olarak DIRECT EXCHANGE kabul edilir.Direct Exchange'te routing key mesaj kuyruğunun ismine karşılık gelir.Yani "example-queue"
//channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Hello " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}

Console.Read();