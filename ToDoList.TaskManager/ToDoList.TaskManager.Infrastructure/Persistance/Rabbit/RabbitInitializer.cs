using System.Reflection;
using ToDoList.TaskManager.Infrastructure.Persistance.DI;
using ToDoList.TaskManager.Infrastructure.Persistance.Interfaces;

namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

public class RabbitInitializer
{
    private readonly RabbitOperationCatalog _operationCatalog;
    private readonly RabbitConnection _connection;
    private readonly RabbitConsumerFactory _consumerFactory;
    private List<RabbitConsumer> _consumers = [];
    
    public RabbitInitializer(
        RabbitOperationCatalog operationCatalog,
        RabbitConnection connection,
        RabbitConsumerFactory consumerFactory)
    {
        _operationCatalog = operationCatalog;
        _connection = connection;
        _consumerFactory = consumerFactory;
    }
    
    public async Task<List<RabbitConsumer>> InitializeAsync(CancellationToken cancellationToken)
    {
        var assembly = typeof(InfrastructureAssemblyMarker).Assembly;
        
        await _connection.GetOrCreateConnectionAsync(cancellationToken);
        
        _operationCatalog.RegisterOperations(assembly);

        _consumers = await _consumerFactory.BuildConsumerAsync(assembly, cancellationToken);

        return _consumers;
    }

    public List<RabbitConsumer> GetInitializedConsumers()
    {
        if (_consumers.Count == 0)
            throw new InvalidOperationException("Consumers was not initialized");

        return _consumers;
    }
}