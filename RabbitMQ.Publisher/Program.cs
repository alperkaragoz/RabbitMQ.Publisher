using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Bağlantıyı oluşturuyoruz.
ConnectionFactory factory = new();
factory.Uri = new("amqps://gagxjjis:g1rs6slF9bx-4tKOXHlP-NVypI8LlG8u@toad.rmq.cloudamqp.com/gagxjjis");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "request-response-queue-example";
channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

// Consumer'dan dönecek olan sonucu elde edeceğimiz kuyruğun adını tanımlıyoruz.
string replyQueueName = channel.QueueDeclare().QueueName;

// Response sürecinde hangi request'e karşılık response'un yapılacağını ifade edecek olan korelasyonel değeri oluşturuyoruz.
string correlationId = Guid.NewGuid().ToString();

IBasicProperties properties = channel.CreateBasicProperties();
properties.CorrelationId = correlationId;
// Response yapılacak queue "ReplyTo" property'sine atanıyor.
properties.ReplyTo = replyQueueName;

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] body = Encoding.UTF8.GetBytes("Request Message " + i);
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: queueName,
        body: body,
        basicProperties: properties
        );

}


//Response kuyruğunu dinleme
EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    if (e.BasicProperties.CorrelationId == correlationId)
        Console.WriteLine($"Response = {Encoding.UTF8.GetString(e.Body.Span)}");
};

Console.Read();