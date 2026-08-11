using RabbitMQ.AMQP.Client;

namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

public class RabbitConsumer
{
    private readonly Func<Func<IContext, IMessage, Task>, Task<IConsumer>> _starter;
    private readonly Func<IContext, IMessage, Task> _compiledHandler;
    private IConsumer? _activeConsumer;

    public string QueueName { get; }
    public string RoutingKey { get; }

    public RabbitConsumer(
        string queueName, 
        string routingKey,
        Func<Func<IContext, IMessage, Task>, Task<IConsumer>> starter,
        Func<IContext, IMessage, Task> compiledHandler)
    {
        QueueName = queueName;
        RoutingKey = routingKey;
        _starter = starter;
        _compiledHandler = compiledHandler;
    }
    
    public async Task StartListeningAsync()
    {
        if (_activeConsumer != null) return;
        _activeConsumer = await _starter(_compiledHandler);
    }

    public void Stop() => _activeConsumer?.Dispose();
}