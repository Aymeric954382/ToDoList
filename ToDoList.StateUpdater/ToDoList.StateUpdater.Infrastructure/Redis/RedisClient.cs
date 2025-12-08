using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.StateUpdater.Application.Interfaces;
using ToDoList.StateUpdater.Application.Stubs;

namespace ToDoList.StateUpdater.Infrastructure.Redis
{
    public class RedisClient : IRedisClient
    {
        private readonly IDatabase _db;
        private const string DeadlinesKey = "zset:deadlines";
        private const string TaskMetaPrefix = "taskmeta:";

        public RedisClient(IConnectionMultiplexer mux)
        {
            _db = mux.GetDatabase();
        }

        public async Task<DeadlineStub[]> PopDueStubsAsync(int batchSize = 100, int leadSeconds = 60)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var targetTime = now + leadSeconds;

            var items = await _db.SortedSetRangeByScoreAsync(
                DeadlinesKey,
                double.NegativeInfinity,
                targetTime,
                Exclude.None,
                Order.Ascending,
                0,
                batchSize
            );

            var result = new List<DeadlineStub>();

            foreach (var item in items)
            {
                if (await _db.SortedSetRemoveAsync(DeadlinesKey, item))
                {
                    var metaKey = TaskMetaPrefix + item.ToString();
                    var hash = await _db.HashGetAllAsync(metaKey);

                    if (hash.Length > 0)
                    {
                        var stub = new DeadlineStub
                        {
                            TaskId = Guid.Parse(hash.First(x => x.Name == "taskId").Value),
                            UserId = Guid.Parse(hash.First(x => x.Name == "userId").Value),
                            DeadLineUnix = (long)hash.First(x => x.Name == "deadline").Value
                        };

                        result.Add(stub);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
