# thy-case

Dokümanda projenin mevcut durumundan bahsedeceğim. Proje belirtilen 3 gün içerisinde <b>tamamlanamadı</b>. Bu yüzden mevcut mimariden, bittiğinde kullanmış olacağım teknolojiler ve implementasyonlardan bahsedeceğim.

Önyüz için belirtildiği şekilde react framework'ü ve typescript şablonunu kullandım. Projenin mevcut arayüzü aşağıdaki şekildedir. 

![alt text](/solution-items/thy-review.png)

t0 anında kapı bilgileri API üzerinden ekranın sağındaki listeye getirilmektedir. Sol kısımda uçakların listesi dummy data olacak şekilde listelenmektedir. Kullanıcı 10'un üzerinde uçak ile simulasyonu başlatabilecektir. Simulasyon başladığında her 5 saniyede bir uçak bir producer yardımı ile RabbitMQ kuyruğuna bırakılacak ve bunu karşılayacak consumer yardımı ile de kapı bulma algoritması çalıştırılacaktır (Bu kısım henüz implemente edilmedi). Server tarafında uçak işlendikten sonra SignalR teknolojisi ile server'dan client'a detay ile ilgili bilgi socket aracılığı ile iletilecektir. Socket bağlantısı kurulu ve server'da yapılan işlemin sonucu client'a gönderilebilecek seviyededir. 

Önyüzün solution view'ı aşağıdaki şekildedir. 

![alt text](/solution-items/react-solution-view.png)

Backend kısmında dotnet 6 kullanılmıştır. 6'yı seçme sebebim LTS olmasıdır. Veritabanı için MsSql server kullandım. ORM için entity framework tercih ettim. Code first yaklaşımı ile db'yi oluşturdum. Uygulama tamamlandığında tüm sistem container'laştırılmış şekilde docker-compose ile ayağa kaldırılabiliyor olacak (veritabanı, api, önyüz, rabbitmq vs). db işlemleri için UOW ile repository'lerimi kapsülledim. Bu sayede transaction yönetimi kolay, db interaction sayısı az olacak. Mevcut durum solution view'ı aşağıdaki şekildedir. 


![alt text](/solution-items/backend-solution-view.png)

Katmanlardan kısaca bahsetmek gerekir ise; 
 
 * THY.GatePlanner.API uygulamanın giriş noktası ve composition root olarak konumlanmaktadır. Dependency injection kullanabilmek için tüm library'ler bu katmanda register edilmektedir.
 * THY.GatePlanner.Infrastructure uygulamanın alt yapı katmanıdır. Veritabanı işlemleri bu katman aracılığı ile yapılmaktadır. 
 * THY.GatePlanner.Model db entity'leri ve DTO diye tabir edebileceğimiz class'ları barındırır.
 * THY.GatePlanner.Service katmanında business implementasyonlar bulunur. 
 ---------

### Eksikler

* Yukarıda bahsedilenler dışında uçtan uca çalışan bir örnek elimde yok. 
* Akış digramı yok.
* Birim testler yok. 
-----------------

### Yorum ve sonraki aşama

Sizden bir haftasonu daha rica edeceğim. Çünkü hafta içi gün içinde çalışıyorum ve akşamları da 19:00-20:00 arasında yoğun bir devops eğitimim var o yüzden haftaiçi vakit ayırma imkanım yok. Bu haftasonu tamamlayamama sebebim ise özel sebeplerin araya girmesi ve farklı plan yapma imkanımın olmamasıydı. 

Sonuca gelecek olursak, bir hafta daha verebilirseniz uygulamanın bitmiş halini görebilirsiniz diye düşünüyorum, vermezseniz canınız sağolsun case benim hoşuma gitti ve bu case'i kafamdaki şekilde tamamlayacağım ben. :)
