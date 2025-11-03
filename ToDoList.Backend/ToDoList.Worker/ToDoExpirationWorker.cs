using ToDoList.Application.Interfaces.Repository;
using ToDoList.Domain.ToDo.ValueObjects;
using ToDoList.Worker.Common.Helper;

namespace ToDoList.Worker
{
    public class ToDoExpirationWorker : BackgroundService
    {
        private readonly IToDoRepository _repository;
        private readonly ILogger<ToDoExpirationWorker> _logger;

        public ToDoExpirationWorker(IToDoRepository repository, ILogger<ToDoExpirationWorker> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ToDoExpirationWorker started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.UtcNow;
                    var todos = _repository.AsQueryable()
                        .Where(t => t.Status != ToDoStatus.Completed && 
                        t.Status != ToDoStatus.Cancelled)
                        .ToList();

                    foreach (var todo in todos)
                    {
                        var percent = StatusCalc.Calc(todo);

                        switch (percent)
                        {
                            case double n when (n >= 33 && n <= 66): todo.Status = ToDoStatus.ExpiringSoon;
                                break;
                            case double n when (n >= 66 && n <= 100): todo.Status = ToDoStatus.Expired;
                                break;
                        }
                        
                        await _repository.UpdateAsync(todo, stoppingToken);

                        _logger.LogInformation($"Task '{todo.Title}' marked as expired.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while processing overdue ToDo items.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
