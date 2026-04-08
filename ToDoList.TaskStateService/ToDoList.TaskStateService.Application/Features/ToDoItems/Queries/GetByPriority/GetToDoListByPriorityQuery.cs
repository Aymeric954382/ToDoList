using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByPriority
{
    public class GetToDoListByPriorityQuery : IQuery<ServiceResult<GetToDoListByPriorityResponseDto>>
    {
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
