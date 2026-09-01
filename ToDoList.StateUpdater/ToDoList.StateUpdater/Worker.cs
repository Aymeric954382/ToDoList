using Microsoft.Extensions.Options;
using ToDoList.StateUpdater.Application.Common.CustomExtensions;
using ToDoList.StateUpdater.Application.Common.Interfaces;
using ToDoList.StateUpdater.Application.Services;
using ToDoList.StateUpdater.Contracts;
using ToDoList.StateUpdater.Contracts.Interfaces;
using Serilog;
using ToDoList.StateUpdater.Worker.Options;

namespace ToDoList.StateUpdater.Worker
{
    public class Worker : BackgroundService
    {
        private readonly DeadlineInMemoryQueue _queue;
        private readonly IUpdateFillter _updateFilter;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WorkerOptions _workerOptions;
        private readonly Serilog.ILogger _logger;

        public Worker(
            DeadlineInMemoryQueue queue,
            IUpdateFillter updateFilter,
            IServiceScopeFactory scopeFactory,
            IOptions<WorkerOptions> options,
            Serilog.ILogger logger)
        {
            _queue = queue;
            _updateFilter = updateFilter;
            _scopeFactory = scopeFactory;
            _workerOptions = options.Value; 
            _logger = logger;
        }
            
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Worker started listening to the queue...");

            var rawItems = new List<DeadlineUpdateDto>(capacity: _workerOptions.MaxBatchSize);

            while (await _queue.Reader.WaitToReadAsync(cancellationToken))
            {
                
                rawItems.Clear();

                _queue.Reader.ReadLimited(rawItems, _workerOptions.MaxBatchSize);

                if (rawItems.Count == 0) continue;

                try
                {
                    var filteredItems = _updateFilter.FilterValidUpdates(rawItems).ToList();

                    if (filteredItems.Count > 0)
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var client = scope.ServiceProvider.GetRequiredService<IGRPCTransferClient>();

                        await client.SendUpdateAsync(filteredItems, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error processing deadline updates batch");

                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
            }
        }
    }
}
                                                                        