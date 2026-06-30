using ToDoList.Gateway.Contracts.ApiClients;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
namespace ToDoList.Gateway.Contracts.Interfaces
{
    public interface ITaskManagerApiClientCommands
    {
        Task<ServiceApiResponse<TaskManagerCreateResponseDto>> CreateAsync(
            TaskManagerCreateRequestDto dto);

        Task<ServiceApiResponse<TaskManagerDeleteResponseDto>> DeleteAsync(
            TaskManagerDeleteRequestDto dto);

        Task<ServiceApiResponse<TaskManagerChangeContentResponseDto>> ChangeContentAsync(
            TaskManagerChangeContentRequestDto dto);
    }
}
