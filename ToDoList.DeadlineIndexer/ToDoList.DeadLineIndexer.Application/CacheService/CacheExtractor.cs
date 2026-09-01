using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Domain;
using Serilog;
using StackExchange.Redis;

namespace ToDoList.DeadlineIndexer.Application.CacheService
{
    public class CacheExtractor
    {
        public readonly IRedisCacheRepository _repository;

        private readonly ILogger _logger;

        public CacheExtractor(
            IRedisCacheRepository repository,
            ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<DeadlineStub>> ExtructCacheAsync(
            CancellationToken cancellationToken)
        {
            try 
            {
                var cache = await _repository.GetCacheItemAsync(cancellationToken);

                var hash = await _repository.GetHashByCacheItemAsync([.. cache], cancellationToken);

                return hash;
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, "Error while extructing cache");

                throw new RedisException("Extructing cache error");
            }
        }
    }
}
