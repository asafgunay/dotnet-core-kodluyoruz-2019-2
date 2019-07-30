# ASP.NET Core ile MVC Web Uygulaması Geliştirme Eğitimi

Kodluyoruz DotNetCore MVC Eğitimi
Başlangıç Tarihi: 03.08.2019
Eğitim Yeri: Ulutek, TİMTEB Girişim Evi

# Eğitim Öncesi Hazırlık

Eğitime gelmeden önce aşağıdaki kurulumlar tamamlanmış ve çalışır durumda olması gerekli.

## Gerekli Araç ve Kurulumlar

* [Visual Studio 2017 veya 2019 Community Kurulumu yapılmalı](https://visualstudio.microsoft.com/tr/vs/)

* [.NET Core SDK için bu linkten kurulumunu yapmalısınız](https://dotnet.microsoft.com/download)

* CMD(Command Line) üzerinde şu kodu çalıştırıp test edin:

```shell
dotnet --version
```
ve sonuç olarak bir versiyon numarası size gönderilmeli.
Aksi durumda kurulum başarısızdır. Adımları kontrol ederek tekrar deneyin.

* Bu noktaya kadar başarılı geldiyseniz Visual Studio’yu açarak aşağıdaki adımları uygulayıp kurulumu test edin (VS2017 üzerinden adımlar anlatılmıştır VS2019’da benzer adımlar ile kolaylıkla erişebilirsiniz) :

1. File ->  New -> Project

2. Açılan ekranda sol taraftan Web seçeneğini seçerek çıkan sonuçlardan bunu seçin : "ASP.NET Core Web Application” ardından "OK" seçeneğini seçin.

3. Proje taslaklarının gösterildiği bir panel açılacak buradan “Empty” seçeneğini seçiniz.

4. Proje yüklendikten sonra menüden Debug -> Start Debugging yapın

5. Tarayıcınız açılacak ve “Hello World” yazısı göründüğünde kurulumunuz başarılı olacaktır.

* [Visual Studio Code Kurulumunu yapın](https://code.visualstudio.com/)

* [Kodluyoruz Bursa #Slack Kanalımıza katılın](https://join.slack.com/t/kodluyoruzbursa/shared_invite/enQtNjIzNzYzNjU2MzM4LThkM2IwM2ZiY2U2YjliMTkxNmZhODcwNTg2NTE3MzhkY2NiNTJhNTkzZDI2NmI0ZmI1YzMwMjkzNjA3M2M5YmQ)

Not: Eğer herhangi bir sorun yaşarsanız email üzerinden benimle iletişime geçebilirsiniz.

# Konu Başlıkları ve İçerik ("Syllabus"):

## Giriş

* .NET CORE SDK Kurulumu
* .NET CORE ile "Merhaba Dünya"
* Yeni bir ASP.NET Core Projesi Oluşturma

## C# Temel Noktaları

## ASP.NET CORE MVC İlk Bakış

* Controller'lar ile çalışma
* Model'ler ile çalışma
* View'ler oluşturma
* Servis sınıfları oluşturma
* Dependency Injection(DI) kullanımı
* Servis sınıflarını Controller sınıfında kullanma
  
## Entity Framework Core ile veri tabanı kullanımı

* Entity Framework Core Giriş
* Veri tabanı kullanma
* Veri Tabanına Bağlanma
* EF Core Migrations Kullanma
* Servis Sınıflarında Context kullanma

## Mini Örnek Uygulama (TODO List)

* Model sınıflarının tanımlanması
* Veri tabanı oluşturma
* Servis sınıfının oluşturulması
* Controller ve View'lerin oluşturulması
* Bootstrap 4.X ile layout ve sayfaların yapılması
* jQuery ile GET, POST, PUT metodlarının tetiklenmesi

## Güvenlik ve Kimlik denetiminin aktifleştirilmesi

* Sosyal medya hesabı ile giriş (Facebook)
* Controller sınıfı metodlarında kimlik kısıtlama
* Veri sahipliği ile kullanıcıları kısıtlama
* Servis sınıflarında kimlik denetiminin sağlanması
* Rol tabanlı yetkilendirme, kısıtlama
  
## Automated Test Yöntemleri

* Neden Unit Testing ve kullanımı
* Neden Integration Testing ve Kullanımı



## Uygulamaların Dağıtılması

* Azure App Service ile uygulamayı yayınlama
* AzureDevops Kullanımı

