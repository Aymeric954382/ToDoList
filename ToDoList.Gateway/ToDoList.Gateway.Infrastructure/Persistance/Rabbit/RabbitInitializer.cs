using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.RabbitEndpoints;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit
{
    public sealed class RabbitInitializer
    {
        private readonly RabbitTopology _topology;

        public RabbitInitializer(RabbitTopology topology) =>
            _topology = topology;
        public async Task InitializeAsync()
        {
            var endoints = RabbitPublisherProvider
                .GetRegisteredEndpointsFromAssembly(typeof(TaskManagerEndpoints).Assembly);

            foreach (var endpoint in endoints)
            {
                await _topology.EnsureTopologyAsync(endpoint);
            }
        }
    }
}
