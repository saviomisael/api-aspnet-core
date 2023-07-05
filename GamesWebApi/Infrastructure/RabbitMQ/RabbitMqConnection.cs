using Infrastructure.RabbitMQ.Options;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMQ;

public static class RabbitMqConnection
{
    private static IConnection? _connection = null;

    public static IConnection GetConnection(RabbitMQOptions options)
    {
        if (_connection is null)
        {
            var connectionFactory = new ConnectionFactory
                { HostName = options.HostName, UserName = options.UserName, Password = options.Password };
        
            _connection = connectionFactory.CreateConnection();
        }

        return _connection;
    }
}