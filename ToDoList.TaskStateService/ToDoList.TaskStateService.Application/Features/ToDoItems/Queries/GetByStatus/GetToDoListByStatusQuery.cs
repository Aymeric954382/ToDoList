using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByStatus
{
    public class GetToDoListByStatusQuery : IQuery<ServiceResult<GetToDoListByStatusResponseDto>>
    {
        public Guid UserId { get; set; }
        public ToDoStatus? Status { get; set; }
    }
}
