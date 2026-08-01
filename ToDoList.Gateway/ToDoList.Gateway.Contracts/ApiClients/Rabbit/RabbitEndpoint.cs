using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.Rabbit
{
    public sealed record RabbitEndpoint(
        string Exchange,
        string QueueName,
        string RoutingKey);
}
