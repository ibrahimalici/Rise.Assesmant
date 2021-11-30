# Rise.Assessment

Uygulama Docker-compose olarak çalışır
ContactaAPI ve ReportAPI olarak iki microservis içerir.
Üçüncü bir api ile (Ocelot Apigateway) gateway verilmiştir.
Mikroservisler arasında ContactAPI - PrepareReport action ı üzerinden RabbitMQ asenkron iletişimi kurularak rapor hazırlanmaktadır.
Apigateway projesine OcelotForSwagger eklentisi eklenmiştir. (Eklenti henüz sağlıklı çalışmıyor)

ContactAPI - swagger
http://localhost:8080/swagger

ReportAPI - swagger
http://localhost:8081/swagger

# Projede kullanılan teknolojiler
.Net Core 
PostgreSQL (Contact database)
MassTransit - RabbitMQ (Asenkron iletişim)
MongoDB (Report database)
Ocelot Apigateway
XUnit - Test Project
