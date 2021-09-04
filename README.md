## TCP - RECTANGLE - GAME

## Özellikler

- Uygulamanın hem Client hemde Server olarak başlatılabilmesi.
- Başlatılan Server'a, manuel olarak ip bilgisinin girilmesiyle client'ın bağlanması.
- Serverdan gelen bilgiye göre client ekranındaki şeklin 20 milisaniyede bir güncellenerek smooth sekilde hareket etmesi.
- Server tarafından gönderilen bilginin hem client hem de server ekranında yazdırılması.

## Kullanılan Teknolojiler

- Socket Programlama
- TCP
- .Net Framework Windows Form

## Nasıl Çalıştırılır

- Github'dan "download zip" seçeneği tıklandıktan sonra inen Zip dosyasının içeriği dışarı alınır.
- Dışarı alınan zip dosyasından, bin -> debug -> WindowsFormApplication2.exe dosyası çalıştırılır.
- Port bilgisi girilir ve server başlatılır. (Thread mekanizmasından dolayı başlatılan server ekranına herhangi bir müdahale yapılamaz.)
- Aynı uygulama bir kez daha çalıştırılır.
- Açılan uygulamada server bilgisine girilen port bilgisi bu sefer client tarafına yazılır ve, Server tarafındaki IP bilgisi Client tarafındaki port bilgisine yazılır.
- Uygulama başlatılr.
- Uygulama açıldıktan sonra server tarafından bilgi gelene kadar iki ekran da boş gelir. Bilgi geldikten sonra kareler oluşur ve server tarafından
gelen bilgiye göre ekrandaki şekiller hareket eder.


## Eksiklikler

- Server'a birden fazla client'ın bağlanamaması.
- Görsellik.






