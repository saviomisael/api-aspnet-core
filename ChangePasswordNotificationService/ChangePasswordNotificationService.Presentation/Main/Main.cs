using Domain.Services;

namespace ChangePasswordNotificationService.Main;

public class Main
{
    private readonly ISendChangePasswordNotificationService _notificationService;

    public Main(ISendChangePasswordNotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    public void Run()
    {
        Console.WriteLine("App is running!");
        
        _notificationService.SendNotification();
        
        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }
}