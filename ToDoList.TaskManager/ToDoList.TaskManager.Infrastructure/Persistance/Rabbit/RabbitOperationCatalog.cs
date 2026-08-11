using System.Reflection;
using ToDoList.TaskManager.Contracts.Common.Interfaces;
using ToDoList.TaskManager.Contracts.Operations.RabbitOperations;

namespace ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

public class RabbitOperationCatalog
{
    private readonly Dictionary<RabbitOperation, Type> _operations = [];
    
    public void RegisterOperations(Assembly assembly)
    {
        var operations = assembly.GetExportedTypes()
            .Where(type => typeof(IRabbitOperation).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .ToList();

        foreach (var operation in operations)
        {
            var field = operation.GetProperty("Operation",BindingFlags.Public | BindingFlags.Static);

            if (field != null)
            {
                var rawValue = field.GetValue(null);
                
                if (rawValue is RabbitOperation operationKey)
                {
                    if (!_operations.TryAdd(operationKey, operation))
                    {
                        throw new InvalidOperationException(
                            $"Rabbit operation '{operationKey}' is already registered.");
                    }
                }
                
                
            }
        }
    }

    public Dictionary<RabbitOperation, Type> GetOperations()
    {
        if (_operations.Count == 0)
            throw new InvalidOperationException("No operations found or search was not performed");
        
        return _operations;
    }
}