using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;
using ToDoList.TaskManager.Contracts.Common.Interfaces;
using ToDoList.TaskManager.Contracts.Operations.RabbitOperations;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommand 
        : IWithResultCommand<ServiceResult<ChangeToDoContentResponseDto>>, 
        IRabbitOperation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public string? Details { get; set; }

        public static RabbitOperation Operation => RabbitOperation.Change;
    }
}
