using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;

namespace ToDoList.Gateway.Application.Interfaces.Orchestartors
{
    public interface IDeleteToDoOrchestrator
    {
        Task<ServiceResult<DeleteToDoResponseDto>> DeleteAsync(DeleteToDoCommand command, CancellationToken cancellationToken);
    }
}
