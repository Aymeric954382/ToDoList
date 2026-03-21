using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommand : IWithResultCommand<ServiceResult<DeleteToDoResponseDto>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
