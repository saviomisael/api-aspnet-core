using System.Text;
using Domain;
using Domain.EmailSender.Services;
using Domain.Entity;
using Infrastructure.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.RabbitMQ;

public class RabbitMqClient
{
    private readonly ISendEmailService _sendEmailService;
    private readonly ConnectionFactory _connectionFactory;

    public RabbitMqClient(ISendEmailService service, RabbitMqOptions options)
    {
        _sendEmailService = service;
        _connectionFactory = new ConnectionFactory
        {
            HostName = options.HostName, UserName = options.UserName, Password = options.Password
        };
    }

    public void ConnectChangePasswordEmailQueue()
    {
        var connection = _connectionFactory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare("change_password_notification", false, false, false, null);
        
        Console.WriteLine("Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            var emailReceiver = JsonConvert.DeserializeObject<EmailReceiver>(message);

            if (emailReceiver is null) return;

            await _sendEmailService.SendEmail(emailReceiver.Email, emailReceiver.UserName);
            Console.WriteLine($"Send email notification for {emailReceiver.Email}.");
        };

        channel.BasicConsume("change_password_notification", true, consumer);
    }
}