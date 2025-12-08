using ToDoList.StateUpdater.Application.Interfaces;

namespace ToDoList.StateUpdater.Worker
{
    public class DueProcessorService : BackgroundService
    {
        private readonly IRedisClient _redisClient;

        public DueProcessorService(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int circleTime = 55;

            while (!stoppingToken.IsCancellationRequested)
            {
                var tasks = await _redisClient.PopDueStubsAsync();

                if (tasks.Length < 0)
                    continue;

                foreach (var task in tasks)
                {
                   //need N 
                }

                
                await Task.Delay(TimeSpan.FromSeconds(circleTime), stoppingToken);
            }
        }
    }
}
