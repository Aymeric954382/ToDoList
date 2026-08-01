using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit.Options
{
    public class RabbitOptions
    {
        public required string Host { get; init; }
        public required int Port { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }
        public required string VirtualHost { get; init; }
    }
}
