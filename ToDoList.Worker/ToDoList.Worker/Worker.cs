using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ToDoList.Worker.Application.Common.Helper;
using ToDoList.Worker.Application.Interfaces.Repository;
using ToDoList.Worker.Domain;
using ToDoList.Worker.Domain.ValueObjects;
using ToDoList.Worker.Infrastructure.Persistance.WorkerConfiguration;

namespace ToDoList.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IToDoRepository _repository;
        private readonly int _intervalSeconds;
        public Worker(IToDoRepository repository, IOptions<WorkerSettings> settings)
        {
            _repository = repository;
            _intervalSeconds = settings.Value.IntervalSeconds;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Worker running at {DateTime.Now}");

                var todos = await _repository.AsQueryable()
                         .Where(t => t.Status != ToDoStatus.Completed &&
                                     t.Status != ToDoStatus.Cancelled)
                         .ToListAsync(stoppingToken);

                var updatedTodos = new List<ToDoItem>();
                foreach (var todo in todos)
                {
                    var remainingPercent = StatusCalc.Calc(todo);
                    var newStatus = StatusRules.GetStatus(remainingPercent);

                    if (newStatus != todo.Status)
                    {
                        todo.Status = newStatus;
                        await _repository.UpdateAsync(todo, stoppingToken);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(_intervalSeconds), stoppingToken);
            }

        }
    }
}

