using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Domain;

namespace ToDoList.DeadlineIndexer.Application.CacheService
{
    public class CacheExtractor
    {
        public readonly IRedisCacheRepository _repository;

        public CacheExtractor(IRedisCacheRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DeadlineStub>> ExtructCacheAsync(CancellationToken cancellationToken)
        {
            try 
            {
                var cache = await _repository.GetCacheItemAsync(cancellationToken);

                var hash = await _repository.GetHashByCacheItemAsync([.. cache], cancellationToken);

                return hash;
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"Cache retrieval error: {ex}");
            }
        }
    }
}
