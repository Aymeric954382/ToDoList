using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries;

namespace ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter
{
    public interface ITaskManagerApiClientAdapter
    {
        //command
        public Task<ServiceResult<TaskManagerChangeContentResponseDto>> ChangeContentAsync(ChangeToDoContentCommand command, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskManagerCreateResponseDto>> CreateAsync(CreateToDoCommand command, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskManagerDeleteResponseDto>> DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken);

        //query
        public Task<ServiceResult<TaskManagerGetToDoListResponseDto>> GetToDoListAsync(GetToDoListQuery query, 
            CancellationToken cancellationToken);

        public Task<ServiceResult<TaskManagerGetToDoListByIdsResponseDto>> GetToDoListByIdAsync(GetToDoListByIdsRequestQuery query,
            CancellationToken cancellationToken);

    }
}
