using ToDoList.DeadlineIndexer.Application.CacheService;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Domain;

namespace ToDoList.DeadlineIndexer.Application.TransferService
{
    public class DeadlineProcessor
    {
        private readonly CacheExtractor _cacheExtractor;
        private readonly IDeadlineNotificationClient _client;
        private readonly IDeadlineDbRepository _repository;
        private readonly IRedisCacheRepository _redisRepository;

        public DeadlineProcessor(
            CacheExtractor cacheExtractor,
            IDeadlineNotificationClient client,
            IDeadlineDbRepository repository,
            IRedisCacheRepository redisRepository)
        {
            _cacheExtractor = cacheExtractor;
            _client = client;
            _repository = repository;
            _redisRepository = redisRepository;
        }

        public async Task ProcessPendingDeadlinesAsync(CancellationToken cancellationToken)
        {
            var stubs = await _cacheExtractor.ExtructCacheAsync(cancellationToken);

            List<DeadLineCache> cache = stubs.Select(stub => new DeadLineCache
            {
                Id = stub.TaskId.GetHashCode(),
                Deadline = DateTimeOffset.FromUnixTimeSeconds(stub.DeadLineUnix).UtcDateTime,
                CreateAt = stub.CreatedAt
            }).ToList();

            if (stubs.Count == 0) return;

            try
            {
                var operationId = Guid.NewGuid();

                await _client.SendDeadlinesAsync(stubs, cancellationToken);

                await _repository.SaveProcessedDeadlinesAsync(cache, operationId, cancellationToken);

                var keysToRemove = stubs.Select(s => (StackExchange.Redis.RedisValue)s.TaskId.ToString()).ToList();
                await _redisRepository.RemoveCacheItemAsync(keysToRemove, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
