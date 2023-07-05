using System.Text;
using Domain.DTO;
using Domain.Service;
using Infrastructure.RabbitMQ.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMQ.Service;

public class SendChangePasswordNotificationService : ISendChangePasswordNotificationService
{
    private readonly RabbitMQOptions _options;
    private const string queue = "change_password_notification";

    public SendChangePasswordNotificationService(RabbitMQOptions options)
    {
        _options = options;
    }
    
    public void SendNotification(EmailReceiverDto dto)
    {
        var connection = RabbitMqConnection.GetConnection(_options);
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue, false, false, false, null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));
        channel.BasicPublish(string.Empty, queue, null, body);
    }
}