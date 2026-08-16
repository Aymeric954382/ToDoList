using MongoDB.Driver;
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

        public MongoInitializer(
            MongoConnectionFactory factory, 
            IMongoClient client)
        {
            _factory = factory;
            _client = client;
        }
        public void Initialize()
        {
            _factory.CreateContext(_client);

            _factory.CreateCollection();
        }
    }
}
