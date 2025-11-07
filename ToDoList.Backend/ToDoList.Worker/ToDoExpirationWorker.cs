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
            _logger.LogInformation("ToDoExpirationWorker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var todos = _repository.AsQueryable()
                        .Where(t => t.Status != ToDoStatus.Completed &&
                                    t.Status != ToDoStatus.Cancelled)
                        .ToList();

                    foreach (var todo in todos)
                    {
                        var remainingPercent = StatusCalc.Calc(todo);
                        var newStatus = todo.Status;

                        if (remainingPercent > 66)
                            newStatus = ToDoStatus.Active;
                        else if (remainingPercent > 0)
                            newStatus = ToDoStatus.ExpiringSoon;
                        else
                            newStatus = ToDoStatus.Expired;

                        if (newStatus != todo.Status)
                        {
                            todo.Status = newStatus;
                            await _repository.UpdateAsync(todo, stoppingToken);
                            _logger.LogInformation($"Task '{todo.Title}' status updated to {newStatus}.");
                        }
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
