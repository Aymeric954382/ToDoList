using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Contracts.Common
{
    public sealed record RabbitEndpoint(
        string Exchange, 
        string QueueName, 
        string RoutingKey);
}
