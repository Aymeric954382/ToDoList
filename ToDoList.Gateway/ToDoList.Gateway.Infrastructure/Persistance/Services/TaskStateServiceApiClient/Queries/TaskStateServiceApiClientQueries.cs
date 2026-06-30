using ToDoList.Gateway.Contracts.ApiClients;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.ServiceQueries;
using ToDoList.Gateway.Contracts.Interfaces;
using ToDoList.Gateway.Infrastructure.Persistance.Helpers.HttpExecutor;
using ToDoList.Gateway.Infrastructure.Persistance.Helpers.RequestFactory;

namespace ToDoList.Gateway.Infrastructure.Persistance.Services.TaskStateServiceApiClient.Queries
{
    public class TaskStateServiceApiClientQueries
        : ITaskStateServiceApiClientQueries
    {
        private readonly HttpExecutor _httpExecutor;
        private readonly TaskStateServiceRoutes _routes;

        public TaskStateServiceApiClientQueries(
            HttpExecutor httpExecutor,
            TaskStateServiceRoutes routes)
        {
            _httpExecutor = httpExecutor;
            _routes = routes;
        }

        public Task<ServiceApiResponse<TaskStateServiceGetToDoListByPriorityResponseDto>> GetToDoListAsync(
            TaskStateServiceGetToDoListRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.GetToDoList,
                dto,
                "GetToDoList",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceGetToDoListByPriorityResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskStateServiceGetToDoListByIdsResponseDto>> GetToDoListByIdAsync(
            TaskStateServiceGetToDoListByIdsRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.GetToDoByIdList,
                dto,
                "GetToDoListByIds",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceGetToDoListByIdsResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskStateServiceGetToDoListByOverdueResponseDto>> GetToDoListByOverdueAsync(
            TaskStateServiceGetToDoListByOverdueRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.GetToDoListByOverdue,
                dto,
                "GetToDoListByOverdue",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceGetToDoListByOverdueResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskStateServiceGetToDoListByPriorityResponseDto>> GetToDoListByPriorityAsync(
            GetToDoListByPriorityRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.GetToDoListByPriority,
                dto,
                "GetToDoListByPriority",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceGetToDoListByPriorityResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskStateServiceGetToDoListByStatusResponseDto>> GetToDoListByStatusAsync(
            TaskStateServiceGetToDoListByStatusRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.GetToDoListByStatus,
                dto,
                "GetToDoListByStatus",
                "TaskStateService");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskStateServiceGetToDoListByStatusResponseDto>>(request);
        }
    }
}