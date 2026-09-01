using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.Redis
{
    public class RedisOptions
    {
        public required string Host { get; init; }
        public required int Port { get; init; }
        public required string Password { get; init; }


        public bool SslCert { get; init; }
        public bool AbortOnConnectFail { get; init; }


        public string Endpoints =>
            $"{Host}:{Port}";
    }
}
