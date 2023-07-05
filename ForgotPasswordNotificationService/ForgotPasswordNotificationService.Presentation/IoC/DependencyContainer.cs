using Application.Service;
using Domain.Service;
using Infrastructure.EmailSender;
using Infrastructure.EmailSender.Service;
using Infrastructure.Options;
using Infrastructure.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForgotPasswordNotificationService.IoC;

public static class DependencyContainer
{
    public static void AddDependencies(this IServiceCollection service)
    {
        service.AddScoped<GmailClient>();
        service.AddScoped<IEmailService, SendRandomPassswordEmailService>();
        service.AddScoped<RabbitMqClient>();
        service.AddScoped<ISendTemporaryPasswordNotificationService, SendTemporaryPasswordNotificationService>();
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