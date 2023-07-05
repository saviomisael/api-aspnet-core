using Domain.Services;
using Infrastructure.RabbitMQ;

namespace Application.Services;

public class SendChangePasswordNotificationService : ISendChangePasswordNotificationService
{
    private readonly RabbitMqClient _rabbitMqClient;

    public SendChangePasswordNotificationService(RabbitMqClient rabbitMqClient)
    {
        _rabbitMqClient = rabbitMqClient;
    }
    
    public void SendNotification()
    {
        _rabbitMqClient.ConnectChangePasswordEmailQueue();
    }
}