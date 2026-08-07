using System.Reflection;
using MediatR;
using RabbitMQ.AMQP.Client;
using Serilog;

namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

public class RabbitConsumerFactory
{
    private readonly RabbitConnection _connection;
    private readonly RabbitOperationCatalog _operationCatalog;
    private readonly RabbitMessageDeserializer _deserializer;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    
    public RabbitConsumerFactory(
        RabbitConnection connection, 
        RabbitOperationCatalog operationCatalog,
        RabbitMessageDeserializer deserializer,
        IMediator mediator,
        ILogger logger)
    {
        _connection = connection;
        _operationCatalog = operationCatalog;
        _deserializer = deserializer;
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task<List<RabbitConsumer>> BuildConsumerAsync(Assembly assembly, CancellationToken cancellationToken)
    {
        _logger.Information("Build consumers was started");
        
        var connection = await _connection.GetOrCreateConnectionAsync(cancellationToken);

        var endpoints = RabbitConsumerProvider.GetRegisteredEndpointsFromAssembly(assembly);
        var operationsMap = _operationCatalog.GetOperations();
        var resultConsumers = new List<RabbitConsumer>();

        var uniqueQueues = endpoints.Select(e => e.QueueName).Distinct();

        foreach (var queueName in uniqueQueues)
        {
            Func<IContext, IMessage, Task> compiledHandler = async (ctx, msg) =>
            {
                try
                {
                    string currentRoutingKey = msg.Property("routing-key")?.ToString() ?? string.Empty;
                    
                    var operationPair = operationsMap
                        .FirstOrDefault(x => x.Key.Value == currentRoutingKey);
                    
                    if (operationPair.Value != null)
                    {
                        string jsonText = msg.BodyAsString();
                        object? dtoMessage = _deserializer.Deserialize(jsonText, operationPair.Value);

                        if (dtoMessage != null)
                        {
                            await _mediator.Send(dtoMessage, cancellationToken);
                        }
                    }
                    
                    ctx.Accept();
                }
                catch (Exception)
                {
                    ctx.Discard();
                }
            };

            Func<Func<IContext, IMessage, Task>, Task<IConsumer>> starter = (handler) =>
            {
                return connection.ConsumerBuilder()
                    .Queue(queueName)
                    .InitialCredits(10)
                    .MessageHandler(new MessageHandler((c, m) => handler(c, m)))
                    .BuildAndStartAsync();
            };

            var consumerWrapper = new RabbitConsumer(queueName, string.Empty, starter, compiledHandler);
            resultConsumers.Add(consumerWrapper);
        }

        return resultConsumers;
    }
}
