using System.Text;
using Domain.DTO;
using Domain.Service;
using Infrastructure.RabbitMQ.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMQ.Service;

public class SendTemporaryPasswordNotificationService : ISendTemporaryPasswordNotificationService
{
    private readonly IConnection _connection;
    private const string QueueName = "forgot_password_notification";

    public SendTemporaryPasswordNotificationService(RabbitMQOptions options)
    {
        _connection = RabbitMqConnection.GetConnection(options);
    }
    
    public void SendNotification(ForgotPasswordEmailReceiverDto dto)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(QueueName, false, false, false, null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));
        channel.BasicPublish(string.Empty, QueueName, null, body);
    }
}