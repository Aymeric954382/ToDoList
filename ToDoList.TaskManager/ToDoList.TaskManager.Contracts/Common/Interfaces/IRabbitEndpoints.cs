using ToDoList.TaskManager.Contracts.EndpointsCommon;

namespace ToDoList.TaskManager.Contracts.Common.Interfaces;

public interface IRabbitEndpoints
{
    IReadOnlyCollection<RabbitEndpoint> GetEndpoints();
}