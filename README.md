# Telephone Directory Microservices Project

Telefon rehberi uygulaması için geliştirilmiş Microservices mimarisini ve Docker kapsayıcılarını temel alan bir .Net 6.0 projesidir. Projede temel CRUD işlemlerine ek olarak asenkron çalışması gereken methodlarda RabbitMQ kuyruk sistemi kullanılmıştır.

## Başlarken

- Repoyu klonlamak için lütfen aşağıdaki kodu kullanın;
```
git clone https://github.com/ademkesim/TelephoneDirectory.git
```
- Ortamınıza docker yüklediğinizden ve yapılandırdığınızdan emin olun. Bundan sonra, aşağıdaki komutları çalıştırabilir ve hemen kullanmaya başlayabilirsiniz.
```powershell
docker-compose build
docker-compose up
```
-Aşağıdaki URL'leri kullanarak uygulamanın farklı bileşenlerine göz atabilmeniz gerekir:
```
Web ApiGateway       : http://localhost:5000/
DirectoryService Api : http://localhost:5001/swagger/index.html
ReportService Api    : http://localhost:5002/swagger/index.html
```

## Geliştirilirken Kullanılanlar

* .NET 6.0
* MongoDB
* Docker
* RabbitMQ
* Ocelot
* Polly
* Moq

## Versiyonlar

Mevcut sürümler için [bu repodaki etiketlere](https://github.com/ademkesim/TelephoneDirectory/tags) bakın.

## Oluşturan

* **Adem Kesim** - *Initial work* - [Profilim](https://github.com/ademkesim)

