using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListQuery : IQuery<ServiceResult<GetToDoListResponseDto>>
    {
        public Guid UserId { get; set; }
    }
}
