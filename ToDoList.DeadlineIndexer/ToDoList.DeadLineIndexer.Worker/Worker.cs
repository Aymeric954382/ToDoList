using Microsoft.Extensions.Options;
using ToDoList.DeadlineIndexer.Application.TransferService;

namespace ToDoList.DeadlineIndexer.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IOptions<WorkerOptions> _options;

    public Worker(ILogger<Worker> logger, 
        IOptions<WorkerOptions> workerOptions,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _options = workerOptions;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var interval = _options.Value.IntervalTimeMinutes;

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            using (var scope = _scopeFactory.CreateScope())
            {
                try
                {
                    var processor = scope.ServiceProvider.GetRequiredService<DeadlineProcessor>();

                    await processor.ProcessPendingDeadlinesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Batch management error");
                }
            } 

            await Task.Delay(interval, stoppingToken);
        }
    }
}
