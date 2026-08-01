using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;
using ToDoList.Gateway.Contracts.Interfaces;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit
{
    public class RabbitPublisherProvider
    {
        public RabbitPublisherProvider(Assembly assembly) =>
            GetRegisteredEndpointsFromAssembly(assembly);

        public static List<RabbitEndpoint> GetRegisteredEndpointsFromAssembly(Assembly assembly)
        {
            var providers = assembly.GetExportedTypes()
                .Where(type => typeof(IRabbitEndpoints).IsAssignableFrom(type))
                .Select(type => (IRabbitEndpoints)Activator.CreateInstance(type)!);

            var endpoints = providers
                .SelectMany(provider => provider.GetEndpoints())
                .ToList();

            return endpoints;
        }
    }
}
