using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListQuery : IQuery<ServiceResult<GetToDoListResponseDto>>
    {
        public Guid UserId { get; set; }
    }
}
