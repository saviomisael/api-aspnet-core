using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForgotPasswordNotificationService.IoC;

public static class DependencyContainer
{
    public static void AddDependencies(this IServiceCollection service)
    {
        
    }

    public static void AddSingletonOptions(this IServiceCollection service, IConfiguration configuration)
    {
        var rabbitmqOptions = new RabbitMqOptions();
        configuration.GetSection("RabbitMQ").Bind(rabbitmqOptions);
        service.AddSingleton(rabbitmqOptions);
        var gmailOptions = new GmailOptions();
        configuration.GetSection("GmailOptions").Bind(gmailOptions);
        service.AddSingleton(gmailOptions);
    }
}