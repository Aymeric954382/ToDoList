using ToDoList.Gateway.Contracts.ApiClients;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.Interfaces;
using ToDoList.Gateway.Infrastructure.Persistance.Helpers.HttpExecutor;
using ToDoList.Gateway.Infrastructure.Persistance.Helpers.RequestFactory;

namespace ToDoList.Gateway.Infrastructure.Persistance.Services.TaskStateServiceApiClient.Commands
{
    public class TaskStateServiceApiClientCommands
        : ITaskStateServiceApiClientCommands
    {
        private readonly HttpExecutor _httpExecutor;
        private readonly TaskStateServiceRoutes _routes;

        public TaskStateServiceApiClientCommands(
            HttpExecutor httpExecutor,
            TaskStateServiceRoutes routes)
        {
            _httpExecutor = httpExecutor;
            _routes = routes;
        }

        public Task<ServiceApiResponse<TaskStateServiceChangeDueDateResponseDto>> ChangeDueDateAsync(
            TaskStateServiceChangeDueDateRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Patch,
                _routes.ChangeDueDate,
                dto,
                "ChangeDueDate",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceChangeDueDateResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskStateServiceChangePriorityResponseDto>> ChangePriorityAsync(
            TaskStateServiceChangePriorityRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Patch,
                _routes.ChangePriority,
                dto,
                "ChangePriority",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceChangePriorityResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskStateServiceChangeStatusResponseDto>> ChangeStatusAsync(
            TaskStateServiceChangeStatusRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Patch,
                _routes.ChangeStatus,
                dto,
                "ChangeStatus",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceChangeStatusResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskStateServiceCreateResponseDto>> CreateAsync(
            TaskStateServiceCreateRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.Create,
                dto,
                "CreateToDoState",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceCreateResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskStateServiceDeleteResponseDto>> DeleteAsync(
            TaskStateServiceDeleteRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.Delete,
                dto,
                "DeleteToDoState",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceDeleteResponseDto>>(request);
        }
    }
}