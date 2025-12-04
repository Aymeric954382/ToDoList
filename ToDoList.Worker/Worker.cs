using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using ToDoList.Worker.Application.Common.Helper;
using ToDoList.Worker.Application.Interfaces.Repository;
using ToDoList.Worker.Domain;
using ToDoList.Worker.Domain.ValueObjects;
using ToDoList.Worker.Infrastructure.Persistance.WorkerConfiguration;
using IDatabase = StackExchange.Redis.IDatabase;

namespace ToDoList.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IToDoRepository _repository;
        private readonly IDatabase _redis;
        private readonly int _intervalSeconds;

        public Worker(
            IToDoRepository repository,
            IConnectionMultiplexer redis,
            IOptions<WorkerSettings> settings)
        {
            _repository = repository;
            _redis = redis.GetDatabase();
            _intervalSeconds = settings.Value.IntervalSeconds;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Worker running at {DateTime.Now}");

                var activeUserIds = await GetActiveUsersAsync();

                if (activeUserIds.Count == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(_intervalSeconds), stoppingToken);
                    continue;
                }

                var todos = await _repository.AsQueryable()
                    .Where(t =>
                        activeUserIds.Contains(t.UserId) &&
                        t.Status != ToDoStatus.Completed &&
                        t.Status != ToDoStatus.Cancelled)
                    .ToListAsync(stoppingToken);

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

        private async Task<List<Guid>> GetActiveUsersAsync()
        {
            var members = await _redis.SetMembersAsync("active-users");
            return members
                .Select(m => Guid.Parse(m.ToString()))
                .ToList();
        }
    }
}

