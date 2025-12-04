using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.interfaces;

namespace ToDoList.Gateway.Infrastructure.Persistance.Redis
{
    public class RedisClient : IRedisClient
    {
        private readonly IDatabase _db;

        public RedisClient(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }
        public Task<bool> DeleteAsync(string key)
        {
            return _db.StringDeleteAsync(key, ValueCondition.Exists, CommandFlags.None);
        }

        public async Task<string?> GetStringAsync(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
           return _db.StringSetAsync(key, value, expiry, When.Always);
        }
    }
}
