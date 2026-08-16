using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.Redis
{
    public class RedisConnectionFactory
    {
        private IConnectionMultiplexer _connection;

        private readonly IDatabase _database;

        public RedisConnectionFactory(IOptions<RedisOptions> options)
        {
            var redis = options.Value;

            var redisOptions = new ConfigurationOptions
            {
                EndPoints = { redis.Endpoints },
                Password = redis.Password,
                Ssl = redis.SslCert,
                AbortOnConnectFail = redis.AbortOnConnectFail,
            };

            _connection = ConnectionMultiplexer.Connect(redisOptions);
        }

        public IDatabase GetDatabase() => _connection.GetDatabase();
    }
}
