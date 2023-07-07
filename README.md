# Games API

This is a project that uses DDD, microservices, RabbitMQ and .NET 6. This project was tested with Postman and XUnit.

# Technologies

- [Docker](https://docs.docker.com/engine/)
- [Docker Compose](https://docs.docker.com/compose/)
- [JWT](https://jwt.io/)
- [Swagger](https://swagger.io/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [Fluent Validation](https://docs.fluentvalidation.net/en/latest/)
- [Entity Framework Core Lazy Loading](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Proxies/6.0.19)
- [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows)
- [Fluent Assertions](https://fluentassertions.com/)
- [Microsoft.AspNetCore.Mvc.Testing](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0)
- [Moq](https://github.com/moq/moq)
- [XUnit](https://xunit.net/)
- [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net/tree/main)
- [Sql Server 2022](https://www.microsoft.com/pt-br/sql-server/sql-server-2022)
- [Full Text Search](https://learn.microsoft.com/en-us/sql/relational-databases/search/full-text-search?view=sql-server-ver16)
- [Postman](https://www.postman.com/downloads/)

## Design Patterns
- `DTO`
- `Mapper`
- `Service`
- `Entity`
- `Repository`
- `Value Object`
- `Singleton`
- `Inversion of Control`
- `Unit of Work`

## How to run

1. Initialize ImagesServer project: ```cd ImagesServer && docker compose up -d```
2. Initialize Sql Server for GamesWebApi and RabbitMQ: ```cd GamesWebApi && docker compose up -d```. Run GamesWebApi: ```cd GamesWebApi/GamesWebApi.Presentation && dotnet run```
3. In another terminal run ChangePasswordNotificationService: ```cd ChangePasswordNotificationService/ChangePasswordNotificationService.Presentation && dotnet run```
4. In another terminal run ForgotPasswordNotificationService: ```cd ForgotPasswordNotificationService/ForgotPasswordNotificationService.Presentation && dotnet run```
5. Access [http://localhost:5002/swagger/index.html](http://localhost:5002/swagger/index.html) to get Swagger documentation
