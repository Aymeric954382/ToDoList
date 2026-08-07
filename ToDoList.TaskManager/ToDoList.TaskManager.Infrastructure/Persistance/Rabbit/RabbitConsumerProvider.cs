using System.Reflection;
using ToDoList.TaskManager.Contracts.Common.Interfaces;
using ToDoList.TaskManager.Contracts.EndpointsCommon;

namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

public class RabbitConsumerProvider
{
    public RabbitConsumerProvider(Assembly assembly) =>
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