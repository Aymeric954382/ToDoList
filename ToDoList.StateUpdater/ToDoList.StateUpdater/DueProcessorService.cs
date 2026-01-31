using ToDoList.StateUpdater.Application.Interfaces;
using ToDoList.StateUpdater.Application.Interfaces.Exceptions;
using ToDoList.StateUpdater.Contracts.ApiClients.Interfaces;
using ToDoList.StateUpdater.Contracts.ApiClients.RequestDto;

namespace ToDoList.StateUpdater.Worker
{
    public class DueProcessorService : BackgroundService
    {
        private readonly IRedisClient _redisClient;
        private readonly ITaskStateClientApiClient _apiClient;
        public DueProcessorService(IRedisClient redisClient, ITaskStateClientApiClient apiClient)
        {
            _redisClient = redisClient;
            _apiClient = apiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            const int circleTime = 55;

            while (!stoppingToken.IsCancellationRequested)
            {
                var tasks = await _redisClient.PopDueStubsAsync();

                if (tasks == null || tasks.Length == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(circleTime), stoppingToken);
                    continue;
                }

                var operations = tasks.Select(task =>
                {
                    var dto = new UpdateToDoDeadLinesRequestDto
                    {
                        Id = task.TaskId,
                        UserId = task.UserId
                    };

                    return ProcessOne(dto, stoppingToken);
                });

                await Task.WhenAll(operations);

                await Task.Delay(TimeSpan.FromSeconds(circleTime), stoppingToken);
            }
        }

        private async Task ProcessOne(UpdateToDoDeadLinesRequestDto dto, CancellationToken token)
        {
            try
            {
                await _apiClient.UpdateDeadLines(dto, token);
            }
            catch (Exception)
            {
                throw new FailSendUpdateDtoException();
            }
        }
    }
}
