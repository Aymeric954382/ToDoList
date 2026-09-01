using Microsoft.Extensions.Options;
using ToDoList.DeadlineIndexer.Application.TransferService;
using Serilog;

namespace ToDoList.DeadlineIndexer.Worker;

public class Worker : BackgroundService
{
    private readonly Serilog.ILogger _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(Serilog.ILogger logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.Information("Worker running at: {time}", DateTimeOffset.Now);

            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var processor = scope.ServiceProvider.GetRequiredService<DeadlineProcessor>();
                    await processor.ProcessPendingDeadlinesAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                _logger.Information("Worker is stopping via cancellation token.");
                break;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Processing troubles");
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (OperationCanceledException)
            {

            }
        }
    }
}

