using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;
using ToDoList.TaskManager.Contracts.Common.Interfaces;
using ToDoList.TaskManager.Contracts.Operations.RabbitOperations;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoCommand 
        : IWithResultCommand<ServiceResult<CreateToDoResponseDto>>,
            IRabbitOperation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public string? Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }
        
        public static RabbitOperation Operation => RabbitOperation.Create;
    }
}
