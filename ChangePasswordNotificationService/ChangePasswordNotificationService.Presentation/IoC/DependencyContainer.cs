using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChangePasswordNotificationService.IoC;

public static class DependencyContainer
{
    public static void AddDependencies(this IServiceCollection service)
    {
        service.AddTransient<Main.Main>();
    }

    public static void AddSingletonOptions(this IServiceCollection service, IConfiguration config)
    {
        var rabbitmqOptions = new RabbitMqOptions();
        config.GetSection("RabbitMQ").Bind(rabbitmqOptions);
        service.AddSingleton(rabbitmqOptions);
    }
}