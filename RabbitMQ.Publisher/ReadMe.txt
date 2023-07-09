	
	
	RabbitMQ
	
	- Gönderici ve alıcı var.gönderici elindeki datayı rabbitmq'ya gönderir.rabbitmq da sırası geldiğinde ilgili alıcıya ilgili mesajı teslim ediyor.
	- Message Broker > Mesaj kuyruk sistemlerine verilen genel isimdir.
	
	- Request-Response süresini azaltmak için kullanabiliriz.
	- Microservice mimarilerde asenkron iletişim sağlamak için kullanabiliriz.
	
	
	- docker'da rabbitmq kurulumu; powershell içerisinde; docker run -d -p 5672:5672 -p 15672:15672 --name rabbitmqcontainer rabbitmq 3.12.0-management
	
	
	* Direct Exchange
	  Routing Key kullanılır.
	
	* Fanout Exchange
	  Örnek bütün kullanıcılara borsa bilgilerini göndermek istiyorsak bu exchange'i kullanabiliriz.
	  
	* Topic Exchange
	  Örnek olarak log sistemi senaryoları için bu exchange'i kullanabiliriz.Kuyruklar, log seviyelerine göre abone olabilir ve sadece routing key'ine uygun ilgili log servisine ait mesajları alabilirler.
	  
	* Header Exchange
	  Routing Key yerine header'ları kullanarak mesajları kuyruklara yönlendirmek için kullanılan exchange'dir.Topicten farkı topic'te routing key kullanılırken header exchange'te headers denilen keyler kullanılır.key-value mantığında kullanılır.
	  
	- Microservice mimarilerinde rabbitmq kullanacaksak eğer, enterprise service bus kütüphanesini kullanacağız.Mass transit kütüphanesi.
	
	
	- Öncelikle RabbitMQ sunucusuna bağlantı oluşturuyoruz.
	- Ardından Bağlantıyı aktifleştireceğiz.
	- Mesajların gönderileceği kuyruğu oluşturacağız.
	
	- Round-Robin Dispatching
	  RabbitMQ tüm consumerlara sırasıyla mesaj gönderir.FIFO(First In First Out).
	  
	- Message Acknowledgement
	  Mesaj onaylama.Tüketiciye gönderilen herhangi bir mesaj ister başarılı ister başarısız olsun kuyruktan silinmesi üzerine işaretler.Default olarak böyledir.
	  Mesajların gönderilmesinde hata olması durumunda o mesajın kuyruğa bildiride bulunulması gerekmektedir.Consumerdan onay bildirisi gelene kadar bekletiyoruz.Gelmiyorsa bu mesajı başka bir consumer tarafından tekrardan kuyruğa ekle.Eğer mesaj başarılı bir şekilde gerçekleştirildiyse RabbitMQ'ya mesajın başarılı olduğu bilgisini iletmeliyiz.Aksi takdirde mesaj 1'den fazla kez işlenecektir.30 dk içerisinde geri bildirimi göndermeliyiz, yoksa RabbitMQ ilgili mesajı tekrardan kuyruğa alacak ve başka bir consumer tarafından işlenmesini sağlayacaktır.Message Acknowledgement BasicAck fonksiyonu ile yapılandırabiliyoruz.Message Acknowledgement Consumer'da tasarlanır.
	  Örn: channel.BasicConsume(queue: "example-queue", false, consumer); BasicConsume içerisindeki ikinci parametre BasicAck propertysidir ve false yaparak Message Acknowledgement özelliğini aktifleştiririz.Otomatik silinme durumunu pasifleştirip mesaj onaylama sürecini aktifleştirmek istiyorsak öncelikle Consumer'da AutoAck parametresini false olarak işaretlememiz gerekir.
	  Sonrasında RabbitMQ'da BasicAck fonksiyonunu çağırarak bildiride bulunur. Örnek kod; channel.BasicAck(e.DeliveryTag,multiple:false); multiple parametresi 1'den fazla mesaja dair onay bildirisi gönderir.Eğer true değeri verilirse,DeliveryTag değerine sahip olan bu mesajla birlikte bundan önce ki mesajların da işlendiğini onaylar.false verilirse şayet, sadece bu mesaj için onay bildirisinde bulunacaktır.