using Domain.Services;
using Microsoft.Extensions.Hosting;

namespace ChangePasswordNotificationService.Main;

public class Main : BackgroundService
{
    private readonly ISendChangePasswordNotificationService _notificationService;

    public Main(ISendChangePasswordNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("App is running!");
        try
        {
            _notificationService.SendNotification();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return Task.CompletedTask;
    }
}