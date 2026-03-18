using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo
{
    public class GetToDoListQuery : IQuery<ServiceResult<GetToDoListResponseDto>>
    {
        public Guid UserId { get; set; }
        
    }
}
