using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;

namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskStateServiceApiClientCommands
    {
        Task<TaskStateServiceCreateResponseDto> CreateAsync(TaskStateServiceCreateRequestDto dto);
        Task<TaskStateServiceDeleteResponseDto> DeleteAsync(TaskStateServiceDeleteRequestDto dto);
        Task<TaskStateServiceChangePriorityResponseDto> ChangePriorityAsync(TaskStateServiceChangePriorityRequestDto dto);
        Task<TaskStateServiceChangeStatusResponseDto> ChangeStatusAsync(TaskStateServiceChangeStatusRequestDto dto);
        Task<TaskStateServiceChangeDueDateResponseDto> ChangeDueDateAsync(TaskStateServiceChangeDueDateRequestDto dto);

    }
}
