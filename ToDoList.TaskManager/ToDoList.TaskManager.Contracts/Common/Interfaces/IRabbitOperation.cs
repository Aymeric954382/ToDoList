using ToDoList.TaskManager.Contracts.Operations.RabbitOperations;

namespace ToDoList.TaskManager.Contracts.Common.Interfaces;

public interface IRabbitOperation 
{
    static abstract RabbitOperation Operation { get; }
}