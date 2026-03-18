using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.ValueObjects;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority
{
    public class GetToDoListByPriorityQuery : IQuery<ServiceResult<GetToDoListByPriorityResponseDto>>
    {
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }

    }
}
