namespace ToDoList.TaskManager.Contracts.EndpointsCommon;

public sealed record RabbitEndpoint(
    string Exchange,
    string QueueName,
    string RoutingKey);