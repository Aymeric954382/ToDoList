using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;
using ToDoList.TaskManager.Contracts.Common.Interfaces;
using ToDoList.TaskManager.Contracts.Operations.RabbitOperations;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommand 
        : IWithResultCommand<ServiceResult<DeleteToDoResponseDto>>,
            IRabbitOperation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        
        public static RabbitOperation Operation =>  RabbitOperation.Delete;
    }
}
