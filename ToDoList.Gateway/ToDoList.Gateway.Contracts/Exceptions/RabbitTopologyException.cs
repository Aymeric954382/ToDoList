using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;

namespace ToDoList.Gateway.Contracts.Exceptions
{
    public class RabbitTopologyException : Exception
    {
        public RabbitTopologyException(RabbitEndpoint endpoint, Exception innerException)
            : base(
                $"Failed to declare Rabbit topology. " +
                  $"Exchange='{endpoint.Exchange}', " +
                  $"Queue='{endpoint.QueueName}', " +
                  $"RoutingKey='{endpoint.RoutingKey}'.",
                innerException)
        {
        }
    }
}
