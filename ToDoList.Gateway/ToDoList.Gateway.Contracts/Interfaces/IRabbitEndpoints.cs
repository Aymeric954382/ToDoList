using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;

namespace ToDoList.Gateway.Contracts.Interfaces
{
    public interface IRabbitEndpoints
    {
        IReadOnlyCollection<RabbitEndpoint> GetEndpoints();
    }
}
