using ToDoList.Gateway.Contracts.ApiClients;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries;
using ToDoList.Gateway.Contracts.Interfaces;
using ToDoList.Gateway.Infrastructure.Persistance.Helpers.HttpExecutor;
using ToDoList.Gateway.Infrastructure.Persistance.Helpers.RequestFactory;

namespace ToDoList.Gateway.Infrastructure.Persistance.Services.TaskManagerApiClient.Queries
{
    public class TaskManagerApiClientQueries
        : ITaskManagerApiClientQueries
    {
        private readonly HttpExecutor _httpExecutor;
        private readonly TaskManagerRoutes _routes;

        public TaskManagerApiClientQueries(
            HttpExecutor httpExecutor,
            TaskManagerRoutes routes)
        {
            _httpExecutor = httpExecutor;
            _routes = routes;
        }

        public Task<ServiceApiResponse<TaskManagerGetToDoListResponseDto>> GetToDoListAsync(
            TaskManagerGetToDoListRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.GetToDoList,
                dto,
                "GetToDoList",
                "TaskManager");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskManagerGetToDoListResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskManagerGetToDoListByIdsResponseDto>> GetToDoListByIdAsync(
            TaskManagerGetToDoListByIdsRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.GetToDoByIdList,
                dto,
                "GetToDoListByIds",
                "TaskManager");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskManagerGetToDoListByIdsResponseDto>>(request);
        }
    }
}