using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskManagerApiClientCommands
    {
        Task<TaskManagerCreateResponseDto> CreateAsync(TaskManagerCreateRequestDto dto);
        Task<TaskManagerDeleteResponseDto> DeleteAsync(TaskManagerDeleteRequestDto dto);
        Task<TaskManagerChangeContentResponseDto> ChangeContentAsync(TaskManagerChangeContentRequestDto dto);

    }
}
