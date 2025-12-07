using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Services.Redis;

namespace ToDoList.Gateway.Infrastructure.Persistance.Redis
{
    using StackExchange.Redis;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ToDoList.Gateway.Contracts.ApiClients.Stubs;

    public class RedisClient : IRedisClient
    {
        private readonly IDatabase _db;
        private const string DeadlinesKey = "zset:deadlines";
        private const string TaskMetaPrefix = "taskmeta:";

        public RedisClient(IConnectionMultiplexer mux)
        {
            _db = mux.GetDatabase();
        }
        public async Task AddDeadlineStubAsync(DeadLineStub stub)
        {
            var metaKey = TaskMetaPrefix + stub.TaskId.ToString("N");

            var entries = new HashEntry[]
            {
            new HashEntry("taskId", stub.TaskId.ToString()),
            new HashEntry("userId", stub.UserId.ToString()),
            new HashEntry("deadline", stub.DeadlineUnix),
            new HashEntry("createdAt", DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            await _db.HashSetAsync(metaKey, entries);

            var ttlSeconds = Math.Max((stub.DeadlineUnix - DateTimeOffset.UtcNow.ToUnixTimeSeconds()) + 3600, 3600);
            await _db.KeyExpireAsync(metaKey, TimeSpan.FromSeconds(ttlSeconds));

            await _db.SortedSetAddAsync(DeadlinesKey, stub.TaskId.ToString(), stub.DeadlineUnix);
        }
    }


}
