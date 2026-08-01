using RabbitMQ.AMQP.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit.Options
{
    public class RabbitTopologyOptions
    {
        public required QueueType QueueType { get; init; }
        public required bool AutoDelete { get; init; }
        public required ushort MaxLength { get; init; }
    }
}
