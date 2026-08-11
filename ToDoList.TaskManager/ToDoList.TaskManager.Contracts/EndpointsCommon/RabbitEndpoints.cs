using ToDoList.TaskManager.Contracts.Common.Interfaces;
using ToDoList.TaskManager.Contracts.Operations.RabbitOperations;

namespace ToDoList.TaskManager.Contracts.EndpointsCommon;

public sealed class RabbitEndpoints : IRabbitEndpoints
{
    public static readonly RabbitEndpoint Create =
        new("commands", "taskmanager.commands", RabbitOperation.Create.Value);

    public static readonly RabbitEndpoint Delete =
        new("commands", "taskmanager.commands", RabbitOperation.Delete.Value);

    public static readonly RabbitEndpoint Change =
        new("commands", "taskmanager.commands", RabbitOperation.Change.Value);

    public IReadOnlyCollection<RabbitEndpoint> GetEndpoints() =>
    [
        Change,
        Create,
        Delete
    ];
}