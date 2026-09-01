using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Serilog;
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

        private readonly ILogger _logger;

        public RedisConnectionFactory(
            IOptions<RedisOptions> options,
            ILogger logger)
        {
            _logger = logger;

            var redis = options.Value;

            var redisOptions = new ConfigurationOptions
            {
                EndPoints = { redis.Endpoints },
                Password = redis.Password,
                Ssl = redis.SslCert,
                AbortOnConnectFail = redis.AbortOnConnectFail,
            };

            _connection = ConnectionMultiplexer.Connect(redisOptions);
            _database = _connection.GetDatabase();
        }

        public IDatabase GetDatabase()
        {
            if (_database == null)
            {
                _logger.Fatal("No connection to Redis db");

                throw new RedisException("No connection to Redis db");
            }
            else
            {
                return _database;
            }
        }
            
    }
}
