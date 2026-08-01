using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;

namespace ToDoList.Gateway.Application.Interfaces.Rabbit
{
    public interface IRabbitPublisher
    {
        Task PublishAsync<T>(
            RabbitEndpoint endpoint,
            T message,
            CancellationToken cancellationToken = default);
    }
}
