using Microsoft.Extensions.Options;
using RabbitMQ.AMQP.Client;
using RabbitMQ.AMQP.Client.Impl;
using ToDoList.TaskManager.Infrastructure.Persistance.Rabbit.Options;

namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

public class RabbitConnection
{
    private IEnvironment _environment;
    
    private IConnection _connection;
    
    private ConnectionSettings _connectionSettings;

    public RabbitConnection(IOptions<RabbitOptions> options)
    {
        RabbitOptions rabbit = options.Value;

        _connectionSettings = ConnectionSettingsBuilder.Create()
            .Host(rabbit.Host)
            .Port(rabbit.Port)
            .User(rabbit.UserName)
            .Password(rabbit.Password)
            .VirtualHost(rabbit.VirtialHost)
            .Build();
    }

    public async Task<IConnection> GetOrCreateConnectionAsync(CancellationToken cancellationToken)
    {
        if (_connection != null)
            return _connection;

        try
        {
            _environment = AmqpEnvironment.Create(_connectionSettings);
            
            _connection = await _environment.CreateConnectionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ConnectionException(
                "Failed to connect to RabbitMQ.",
                ex);
        }
        
        return _connection;
    }
}