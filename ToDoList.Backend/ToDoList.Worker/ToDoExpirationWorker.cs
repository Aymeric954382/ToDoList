using ToDoList.Application.Interfaces.Repository;
using ToDoList.Domain.ToDo.ValueObjects;
using ToDoList.Worker.Common.Helper;

namespace ToDoList.Worker
{
    public class ToDoExpirationWorker : BackgroundService
    {
        private readonly IToDoRepository _repository;
        public ToDoExpirationWorker(IToDoRepository repository)
        {
            _repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

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
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Log
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
