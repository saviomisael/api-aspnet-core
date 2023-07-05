using System.Text;
using Domain.Entity;
using Domain.Service;
using Infrastructure.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.RabbitMQ;

public class RabbitMqClient
{
    private readonly IEmailService _sendEmailService;
    private readonly ConnectionFactory _connectionFactory;
    private const string QueueName = "forgot_password_notification";

    public RabbitMqClient(IEmailService service, RabbitMqOptions options)
    {
        _sendEmailService = service;
        _connectionFactory = new ConnectionFactory
        {
            HostName = options.HostName, UserName = options.UserName, Password = options.Password
        };
    }
    
    public void ConnectForgotPasswordEmailQueue()
    {
        var connection = _connectionFactory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(QueueName, false, false, false, null);
        
        Console.WriteLine("Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            var emailReceiver = JsonConvert.DeserializeObject<EmailReceiver>(message);

            if (emailReceiver is null) return;

            await _sendEmailService.SendEmail(emailReceiver);
            Console.WriteLine($"Send email notification for {emailReceiver.Email}.");
        };

        channel.BasicConsume(QueueName, true, consumer);
    }
}