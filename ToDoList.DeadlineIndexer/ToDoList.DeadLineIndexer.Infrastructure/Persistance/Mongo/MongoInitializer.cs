using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.Mongo
{
    public class MongoInitializer
    {
        public readonly MongoConnectionFactory _factory;

        public readonly IMongoClient _client;
        private readonly ILogger _logger;

        public MongoInitializer(
            MongoConnectionFactory factory, 
            IMongoClient client,
            ILogger logger)
        {
            _factory = factory;
            _client = client;

            _logger = logger;
        }
        public void Initialize()
        {
            _logger.Information("Initializing mongo db");

            _factory.CreateContext(_client);

            _factory.CreateCollection();
        }
    }
}
