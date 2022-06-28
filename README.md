# MonifiBackend

Modular Monolith basit bir şablon uygulamasıdır.


# Özellikler

 - HealthCheck
 - Rate Limiting
 - DDD Design Pattern
 - CQRS Design Pattern
 - MediatR
 - Entity Framework

## Bilgisayarınızda Çalıştırın

  Projeyi klonlayın
  > git clone https://github.com/BerkanARIKAN/monifibackend

Proje dizinine gidin
  > cd monifibackend

Gerekli paketleri yükleyin
  > dotnet restore

Projelerin konfiguresyonunu düzeltin
  > src/Presentation/MonifiBackend.API/appsettings.Development.json

Migration işlemini gerçekleştirin
  > cd src\Presentation\MonifiBackend.API
  
  > dotnet ef migrations add InitialCreate -c MonifiBackendDbContext
  
  > dotnet ef database update -c MonifiBackendDbContext

Projeyi Çalıştırın
  > dotnet run

Testler
  > dotnet test
