using Microsoft.Extensions.Hosting;

namespace ForgotPasswordNotificationService.Main;

public class Main : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}