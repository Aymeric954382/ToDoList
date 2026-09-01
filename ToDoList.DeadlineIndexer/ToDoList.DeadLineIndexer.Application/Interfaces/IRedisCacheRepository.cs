using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DeadlineIndexer.Domain;

namespace ToDoList.DeadlineIndexer.Application.Interfaces
{
    public interface IRedisCacheRepository
    {
        Task<IEnumerable<RedisValue>> GetCacheItemAsync(
            CancellationToken cancellationToken);

        Task<List<DeadlineStub>> GetHashByCacheItemAsync(List<RedisValue> items, 
            CancellationToken cancellationToken);

        Task RemoveCacheItemAsync(List<RedisValue> items, 
            CancellationToken cancellationToken);

    }
}
