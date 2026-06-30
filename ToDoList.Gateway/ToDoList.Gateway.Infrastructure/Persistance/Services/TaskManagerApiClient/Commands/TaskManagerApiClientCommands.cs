using System.Net.Http.Json;
using ToDoList.Gateway.Contracts.ApiClients;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.Interfaces;
using ToDoList.Gateway.Infrastructure.Persistance.Helpers.HttpExecutor;
using ToDoList.Gateway.Infrastructure.Persistance.Helpers.RequestFactory;

namespace ToDoList.Gateway.Infrastructure.Persistance.Services.TaskManagerApiClient.Commands
{
    public class TaskManagerApiClientCommands
        : ITaskManagerApiClientCommands
    {
        private readonly HttpExecutor _httpExecutor;
        private readonly TaskManagerRoutes _routes;

        public TaskManagerApiClientCommands(
            HttpExecutor httpExecutor,
            TaskManagerRoutes routes)
        {
            _httpExecutor = httpExecutor;
            _routes = routes;
        }

        public Task<ServiceApiResponse<TaskManagerChangeContentResponseDto>> ChangeContentAsync(
            TaskManagerChangeContentRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Patch,
                _routes.ChangeContent,
                dto,
                "ChangeContent",
                "TaskManager");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskManagerChangeContentResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskManagerDeleteResponseDto>> DeleteAsync(
            TaskManagerDeleteRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Patch,
                _routes.Delete,
                dto,
                "DeleteToDo",
                "TaskManager");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskManagerDeleteResponseDto>>(request);
        }

        public Task<ServiceApiResponse<TaskManagerCreateResponseDto>> CreateAsync(
            TaskManagerCreateRequestDto dto)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.Create,
                dto,
                "CreateToDo",
                "TaskManager");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<TaskManagerCreateResponseDto>>(request);
        }
    }
}