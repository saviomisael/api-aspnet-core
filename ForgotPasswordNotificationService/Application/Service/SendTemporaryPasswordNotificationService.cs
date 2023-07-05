using Domain.Service;
using Infrastructure.RabbitMQ;

namespace Application.Service;

public class SendTemporaryPasswordNotificationService : ISendTemporaryPasswordNotificationService
{
    private readonly RabbitMqClient _rabbitMqClient;

    public SendTemporaryPasswordNotificationService(RabbitMqClient client)
    {
        _rabbitMqClient = client;
    }
    
    public void SendNotification()
    {
        _rabbitMqClient.ConnectForgotPasswordEmailQueue();
    }
}