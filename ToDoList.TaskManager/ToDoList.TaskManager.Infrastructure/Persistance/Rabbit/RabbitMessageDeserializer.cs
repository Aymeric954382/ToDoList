using System.Text.Json;
using ToDoList.TaskManager.Contracts.Operations.RabbitOperations;

namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

public class RabbitMessageDeserializer
{
    public object Deserialize(string jsonMessage, Type operationType)
    {
        object? commandDto = JsonSerializer.Deserialize(jsonMessage, operationType, options: null);

        if (commandDto == null)
        {
            throw new JsonException($"Failed to deserialize JSON into type {operationType.Name}");
        }
        
        return commandDto;
    }
}