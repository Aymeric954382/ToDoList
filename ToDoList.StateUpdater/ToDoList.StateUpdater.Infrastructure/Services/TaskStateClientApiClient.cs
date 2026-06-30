using System.Net.Http.Json;
using ToDoList.StateUpdater.Contracts.ApiClients;
using ToDoList.StateUpdater.Contracts.ApiClients.Interfaces;
using ToDoList.StateUpdater.Contracts.ApiClients.RequestDto;
using ToDoList.StateUpdater.Contracts.ApiClients.ResponseDto;
using ToDoList.StateUpdater.Contracts.ApiClients.Routes;
using ToDoList.StateUpdater.Infrastructure.Helpers.HttpExecutor;
using ToDoList.StateUpdater.Infrastructure.Helpers.RequestFactory;

namespace ToDoList.StateUpdater.Infrastructure.Services
{
    public class TaskStateClientApiClient 
        : ITaskStateClientApiClient
    {
        private readonly HttpExecutor _httpExecutor;
        private readonly TaskStateRoutes _routes;
        public TaskStateClientApiClient( 
            HttpExecutor httpExecutor,
            TaskStateRoutes routes)
        {
            _httpExecutor = httpExecutor;
            _routes = routes;
        }
        public Task<ServiceApiResponse<UpdateToDoDeadLinesResponseDto>> UpdateDeadLines(
            UpdateToDoDeadLinesRequestDto dto, 
            CancellationToken cancellationToken)
        {
            var request = HttpRequestFactory.Create(
                HttpMethod.Post,
                _routes.UpdateDeadLines,
                dto,
                "UpdateDeadLines",
                "TaskState");

            return _httpExecutor.ExecuteAsync<
                ServiceApiResponse<UpdateToDoDeadLinesResponseDto>>(request);
        }
    }
}
