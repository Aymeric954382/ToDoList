using System.Reflection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

public class RabbitConsumersHost : BackgroundService
{
    private readonly RabbitInitializer _initializer;

    private readonly ILogger _logger;
    
    private IReadOnlyList<RabbitConsumer>? _consumers;

    public RabbitConsumersHost(
        RabbitInitializer initializer, 
        ILogger logger)
    {
        _initializer = initializer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumers = _initializer.GetInitializedConsumers();

        foreach (var consumer in _consumers )
        {
            await consumer.StartListeningAsync();
        }

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_consumers is null || _consumers.Count == 0)
        {
            _logger.Warning("No Rabbit consumers to stop.");

            await base.StopAsync(cancellationToken);
            return;
        }
    }
}