namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit.Options;

public class RabbitOptions
{
    public required string Host { get; init; }
    
    public required int Port { get; init; }
    
    public required string UserName { get; init; }
    
    public required string Password { get; init; }
    
    public required string VirtialHost { get; init; }
}