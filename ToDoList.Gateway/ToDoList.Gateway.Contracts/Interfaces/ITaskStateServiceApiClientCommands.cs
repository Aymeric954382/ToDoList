using ToDoList.Gateway.Contracts.ApiClients;
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

namespace ToDoList.Gateway.Contracts.Interfaces
{
    public interface ITaskStateServiceApiClientCommands
    {
        Task<ServiceApiResponse<TaskStateServiceCreateResponseDto>> CreateAsync(TaskStateServiceCreateRequestDto dto);

        Task<ServiceApiResponse<TaskStateServiceDeleteResponseDto>> DeleteAsync(TaskStateServiceDeleteRequestDto dto);

        Task<ServiceApiResponse<TaskStateServiceChangePriorityResponseDto>> ChangePriorityAsync(
            TaskStateServiceChangePriorityRequestDto dto);

        Task<ServiceApiResponse<TaskStateServiceChangeStatusResponseDto>> ChangeStatusAsync(
            TaskStateServiceChangeStatusRequestDto dto);

        Task<ServiceApiResponse<TaskStateServiceChangeDueDateResponseDto>> ChangeDueDateAsync(
            TaskStateServiceChangeDueDateRequestDto dto);
    }
}
