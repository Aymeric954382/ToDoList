using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommand : IWithResultCommand<ServiceResult<ChangeToDoContentResponseDto>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
