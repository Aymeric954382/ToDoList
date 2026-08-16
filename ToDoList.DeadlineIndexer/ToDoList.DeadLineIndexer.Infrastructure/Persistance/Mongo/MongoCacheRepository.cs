using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Domain;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.Mongo
{
    public class MongoCacheRepository : IDeadlineDbRepository
    {
        private readonly IMongoCollection<BsonDocument> _connection;

        public MongoCacheRepository(MongoConnectionFactory connection)
        {
            _connection = connection.GetArchiveCollection();
        }

        public async Task SaveProcessedDeadlinesAsync(List<DeadLineCache> models, Guid transactionId, CancellationToken cancellationToken)
        {
            var documents = models.Select(model => new BsonDocument
            {
                { "_id", model.Id },
                { "deadline", model.Deadline },
                { "createdAt", model.CreateAt },
                { "synchronizedAt", DateTime.UtcNow },
                { "transactionId", transactionId.ToString()}
            }).ToList();

            if (documents.Count > 0)
            {
                await _connection.InsertManyAsync(documents, cancellationToken: cancellationToken);
            }
        }
    }
}
