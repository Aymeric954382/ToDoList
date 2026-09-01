using Microsoft.Extensions.Options;
using StackExchange.Redis;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Domain;
using ToDoList.DeadlineIndexer.Infrastructure.Persistance.ApplicationOptions;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.Redis
{
    public class RedisCacheRepository : IRedisCacheRepository
    {
        private readonly IDatabase _database;

        private const string _DeadlinesKey = "zset:deadlines";
        private const string TaskMetaPrefix = "taskmeta:";

        private readonly int _BatchSize;
        private readonly int _LeadSeconds;

        public RedisCacheRepository(
            RedisConnectionFactory connectionFactory, 
            IOptions<CacheOptions> options)
        {
            _database = connectionFactory.GetDatabase();

            var cacheOptions = options.Value;

            _BatchSize = cacheOptions.BatchSize;
            _LeadSeconds = cacheOptions.LeadSeconds;
        }

        public async Task<IEnumerable<RedisValue>> GetCacheItemAsync(
            CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var targetTime = now + _LeadSeconds;

            var items = await _database.SortedSetRangeByScoreAsync(
                _DeadlinesKey,
                double.NegativeInfinity,
                targetTime,
                Exclude.None,
                Order.Ascending,
                0,
                _BatchSize
            );

            return items;
        }

        public async Task<List<DeadlineStub>> GetHashByCacheItemAsync(
            List<RedisValue> items, 
            CancellationToken cancellationToken)
        {
            var stubs = new List<DeadlineStub>();

            foreach (var item in items)
            {
                var metaKey = TaskMetaPrefix + item.ToString();
                var hash = await _database.HashGetAllAsync(metaKey);

                var stub = new DeadlineStub
                {
                    TaskId = Guid.Parse(hash.First(x => x.Name == "taskId").Value),
                    UserId = Guid.Parse(hash.First(x => x.Name == "userId").Value),
                    DeadLineUnix = (long)hash.First(x => x.Name == "deadline").Value,
                    CreatedAtUnix = (long)hash.First(x => x.Name == "createdAt").Value
                };

                stubs.Add(stub);
            }

            return stubs;
        }

        public async Task RemoveCacheItemAsync(
            List<RedisValue> items, 
            CancellationToken cancellationToken)
        {

            foreach (var item in items)
            {
                await _database.SortedSetRemoveAsync(_DeadlinesKey, item);
            }
            
        }
    }
}
