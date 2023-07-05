using Infrastructure.EmailSender;
using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChangePasswordNotificationService.IoC;

public static class DependencyContainer
{
    public static void AddDependencies(this IServiceCollection service)
    {
        service.AddTransient<Main.Main>();
        service.AddScoped<GmailClient>();
    }

    public static void AddSingletonOptions(this IServiceCollection service, IConfiguration config)
    {
        var rabbitmqOptions = new RabbitMqOptions();
        config.GetSection("RabbitMQ").Bind(rabbitmqOptions);
        service.AddSingleton(rabbitmqOptions);
        var gmailOptions = new GmailOptions();
        config.GetSection("GmailOptions").Bind(gmailOptions);
        service.AddSingleton(gmailOptions);
    }
}