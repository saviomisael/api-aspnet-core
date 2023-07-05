using Infrastructure.ImagesServerApi.Options;
using Infrastructure.Jwt.Options;
using Infrastructure.RabbitMQ.Options;

namespace GamesWebApi.IoC;

public static class OptionsExtensions
{
    public static void AddSingletonOptions(this IServiceCollection service, IConfiguration configuration)
    {
        var imagesServerOptions = new ImagesServerOptions();
        configuration.GetSection("ImagesServerOptions").Bind(imagesServerOptions);
        service.AddSingleton(imagesServerOptions);
        var jwtOptions = new JwtOptions();
        configuration.GetSection("JWT").Bind(jwtOptions);
        service.AddSingleton(jwtOptions);
        var rabbitmqOptions = new RabbitMQOptions();
        configuration.GetSection("RabbitMQ").Bind(rabbitmqOptions);
        service.AddSingleton(rabbitmqOptions);
    }
}