using Domain.Service;
using Microsoft.Extensions.Hosting;

namespace ForgotPasswordNotificationService.Main;

public class Main : BackgroundService
{
    private readonly ISendTemporaryPasswordNotificationService _service;

    public Main(ISendTemporaryPasswordNotificationService service)
    {
        _service = service;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("App is running!");
        try
        {
            _service.SendNotification();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return Task.CompletedTask;
    }
}