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
        public Task<TaskManagerChangeContentResponseDto> ChangeContentAsync(ChangeToDoContentCommand command, 
            CancellationToken cancellationToken);
        public Task<TaskManagerCreateResponseDto> CreateAsync(CreateToDoCommand command, 
            CancellationToken cancellationToken);
        public Task<TaskManagerDeleteResponseDto> DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken);

        //query
        public Task<TaskManagerGetToDoListResponseDto> GetToDoListAsync(GetToDoListQuery query, 
            CancellationToken cancellationToken);

        public Task<TaskManagerGetToDoListByIdsResponseDto> GetToDoListByIdAsync(GetToDoListByIdsRequestQuery query,
            CancellationToken cancellationToken);

    }
}
