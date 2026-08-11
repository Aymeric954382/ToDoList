using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.Rabbit
{
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
}
