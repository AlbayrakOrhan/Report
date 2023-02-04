
# Report

Projeyi Onion architecture mimarisine göre desing etmeye çalıştım.Solution altında yayınlanmaya hazır 2 adet proje bulunuyor.
 - `Report.Api`: .Net 6 Web Api 
 - `Report.Consumer`: .Net 6 Console Application
 
 **Not: Test sırasında 2 uygulamanın da çalışır durumda olması gerekiyor.**

Katmanlar ile ilgili kısaca bilgi vermek gerekirse;
 



 - `Report.Domain`: Tüm uygulama için olan Domain ve veritabanı entity’leri bu katmanda oluşturulur.
 - `Report.Application`: Bu katman, Domain katmanı ile uygulamanın business katmanı diyebileceğimiz arasında bir soyutlama katmanıdır.
 - `Report.Infrastructure`: Sisteme eklenecek dış/external servisler bu katmanda konumlanır.
 - `Report.Persistence`: DbContext, migration ve veritabanı konfigürasyon işlemleri bu katmanda gerçekleştirilir. 
 - `Report.Api`: Kullanıcının uygulama ile iletişime geçtiği katmandır.
 - `Report.Consumer`: RabbitMQ'daki message'ları dinleyen consumer'ları barındıran .Net 6 console uygulamasıdır. 


## API Reference

#### Yeni bir rapor talebi oluşturmak için kullanabileceğiniz endpoint.

```http
  POST /reports
```


#### Yapılmış tüm rapor taleplerini listeleyebileceğiniz endpoint.

```http
  GET /reports
```

## Database

Uygulamada PostgreSql veritabanı kullandım. Ben postgre kurulumunu docker ile yaptım.Çalıştıracağınız ortama göre connectionString'i düzenlemeniz gerekecek. Ben connectionString'im şu şekilde.

```bash
  Host=localhost;Port=54320;Username=postgres;Password=testdb;Database=Contact
```


## How to Work ?

Sırasıyla aşağıdaki işlemleri yaparak test yapabilirsiniz.

- Öncelikle 2 uygulamayı çalıştırın
- Report.Api'deki /reports POST endpointini tetikleyip, yeni bir rapor talebinde bulunabilirsiniz. Bu süreçte, yeni bir rapor talebi (ReportRequest) kaydedildiğinde, bir domain event (ReportCreatedEvent) dispatch ediliyor olacak ve ReportRequestCreatedEventHandler çalışıyor olcak. Bu handler içerisinde de RabbitMQ sunucusuna bir ReportRequestMessage mesaj publish edilecek. Ve kuyruğu dinleyen consumer, ilgili mesajı alıp, Contact.Api'ye http isteği yaparak, ilgili rapor datasını alacak ve daha önce oluşturulmuş olan ReportRequest entity'sinin Status alanını "completed" olarak güncelleyecek.Ayrıca CompletedDate alanını ve rapor dosyasının yolunu tutan ReportPath alanını da güncelleyecek.
- Daha sonra Report.Api deki 2. endpoint olan /reports GET endpointini çağırarak listeleme işlemini yapabilir ve raporların statülerini kontrol edebilirsiniz.

***ÖNEMLİ NOT: Kısıtlı zamanlarda projeye bakmaya çalıştım ve inanılmaz yoğun bir hafta geçirdikten sonra haftasonu çalışmak gerçekten çok zor geldi. Bu sebeple daha kritik gördüğüm noktalara odaklandım fakat daha az kritik sayılabilecek bölümlerde daha özensiz davrandığım oldu.
Örneğin; Excel file işlemleri yapabileceğim implementationları hiç yapmayıp, sanki excel file oluşturmuşum gibi ve kaydettiğim bu dosyanın path'ini almışım gibi, string bir path ataması yaptım.***

***Environment değişkenleri öncelikle kodun içinde kullandım (her şey bittikten sonra düzenleme geçerim diye). Örneğin connection string'i bir secret değerlerinden veya appsettings environment değerlerinden okumadım da, dbcontext'in service kaydını yaptığım kısımda hard coded yazdım.***

***Readme yi çok geç bir saatte oluşturuyorum eğer fırsat bulabilirsem, burda yazmış bu gibi durumları belki daha sonra düzeltmiş olabilirim :)***

