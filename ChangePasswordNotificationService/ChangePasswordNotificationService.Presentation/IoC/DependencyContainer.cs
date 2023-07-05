using Application.Services;
using Domain.EmailSender.Services;
using Domain.Services;
using Infrastructure.EmailSender;
using Infrastructure.EmailSender.Services;
using Infrastructure.Options;
using Infrastructure.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChangePasswordNotificationService.IoC;

public static class DependencyContainer
{
    public static void AddDependencies(this IServiceCollection service)
    {
        service.AddScoped<GmailClient>();
        service.AddScoped<ISendEmailService, ChangePasswordEmailService>();
        service.AddScoped<RabbitMqClient>();
        service.AddScoped<ISendChangePasswordNotificationService, SendChangePasswordNotificationService>();
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