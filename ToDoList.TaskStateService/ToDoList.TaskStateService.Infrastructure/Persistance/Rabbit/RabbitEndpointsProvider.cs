using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Contracts.Common;
using ToDoList.TaskStateService.Contracts.Common.Interfaces;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.Rabbit
{
    public class RabbitEndpointsProvider
    {
        public RabbitEndpointsProvider(Assembly assembly) =>
            GetRegisteredEndpointsFromAssembly(assembly);

        public static List<RabbitEndpoint> GetRegisteredEndpointsFromAssembly(Assembly assembly)
        {
            var providers = assembly.GetExportedTypes()
                .Where(type => typeof(IRabbitEndpoints).IsAssignableFrom(type))
                .Select(type => (IRabbitEndpoints)Activator.CreateInstance(type)!);

            var endpoints = providers
                .SelectMany(providers => providers.GetEndpoints())
                .ToList();

            return endpoints;
        }
    }
}
