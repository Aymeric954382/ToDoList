namespace ToDoList.StateUpdater
{
    public class DueProcessorService : BackgroundService
    {
        private readonly ILogger<DueProcessorService> _logger;

        public DueProcessorService(ILogger<DueProcessorService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
