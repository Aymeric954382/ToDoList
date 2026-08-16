using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.Mongo
{
    public class MongoConnectionFactory
    {
        private IMongoDatabase _database;

        private readonly CollectionConfigurations _collectionConfigurations;
        private readonly MongoOptions _mongoOptions;

        public MongoConnectionFactory(
            IOptions<CollectionConfigurations> collectionConfigurations,
            IOptions<MongoOptions> mongoOptions)
        {
            _mongoOptions = mongoOptions.Value;
            _collectionConfigurations = collectionConfigurations.Value;
        }

        public void CreateContext(IMongoClient client)
        {
            if (_database == null)
                _database = client.GetDatabase(_mongoOptions.MongoDbName);
        }

        public void CreateCollection()
        {
            if (_database != null)
                return;

            var filter = new BsonDocument("name", _collectionConfigurations.CollectionName);
            var collections = _database.ListCollections(new ListCollectionsOptions { Filter = filter });

            if (!collections.Any())
            {
                _database.CreateCollection(_collectionConfigurations.CollectionName);
            }
        }

        public IMongoCollection<BsonDocument> GetArchiveCollection()
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Mongo is not initilized");
            }

            return _database.GetCollection<BsonDocument>(_collectionConfigurations.CollectionName);
        }
    }
}
