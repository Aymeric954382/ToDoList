using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.Mongo
{
    public class MongoOptions
    {
        public const string SectionName = "MongoSettings";

        public required string Host { get; init; }
        public required int Port { get; init; }
        public required string Password { get; init; }
        public required string Username { get; init; }

        public required string MongoDbName { get; init; }

        public string ConnectionString =>
            $"mongodb://{Username}:{Password}@{Host}:{Port}/?authSource=admin";
    }
}
