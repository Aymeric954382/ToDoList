using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Commands
{
    public class TaskManagerApiClientCommands : ITaskManagerApiClientCommands
    {
        private readonly HttpClient _http;
        private readonly TaskManagerApiOptions _options;

        public TaskManagerApiClientCommands(HttpClient http, IOptions<TaskManagerApiOptions> options)
        {
            _http = http;
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (_options.Routes == null)
            {
                throw new InvalidOperationException(
                $"TaskStateServiceApiOptions.Routes is null. Called from " +
                $"{nameof(TaskStateServiceApiClient)} constructor."
                );
            }
        }

        public async Task<HttpResponseMessage> ChangeContentAsync(ChangeToDoContentDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangeContent, dto);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(GetToDoListOverdueDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.Delete, dto);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<GetToDoIdDto> CreateAsync(CreateForManagerToDoDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.Create, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GetToDoIdDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.Create}");
        }
    }

}
