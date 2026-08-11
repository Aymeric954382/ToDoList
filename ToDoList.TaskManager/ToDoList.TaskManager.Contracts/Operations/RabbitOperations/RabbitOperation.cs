namespace ToDoList.TaskManager.Contracts.Operations.RabbitOperations;

public sealed class RabbitOperation
{
    public static readonly RabbitOperation Create = new("taskmanager.create");
    public static readonly RabbitOperation Delete = new("taskmanager.delete");
    public static readonly RabbitOperation Change = new("taskmanager.change-title-description");

    public string Value { get; }

    private RabbitOperation(string value)
    {
        Value = value;
    }
}