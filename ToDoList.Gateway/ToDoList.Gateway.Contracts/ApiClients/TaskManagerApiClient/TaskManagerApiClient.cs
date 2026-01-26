using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient
{
    public class TaskManagerApiClient : ITaskManagerApiClient
    {
        private readonly HttpClient _http;
        private readonly TaskManagerApiOptions _options;

        public TaskManagerApiClient(HttpClient http, IOptions<TaskManagerApiOptions> options)
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

        public async Task<HttpResponseMessage> DeleteAsync(DeleteToDoDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.Delete, dto);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<ToDoResponseDto> CreateAsync(CreateForManagerToDoDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.Create, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.Create}");
        }
    }

}
