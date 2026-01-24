using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;
using ToDoList.Gateway.Contracts.Providers;


namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient
{
    public class TaskStateServiceApiClient : ITaskStateClientApiClient
    {
        private readonly HttpClient _http;
        private readonly TaskStateServiceApiOptions _options;

        public TaskStateServiceApiClient(HttpClient http, IOptions<TaskStateServiceApiOptions> options)
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

        public async Task<ToDoResponseDto> ChangeDueDateAsync(ChangeToDoDueDateDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangeDueDate, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>()
                      ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.ChangeDueDate}");
        }

        public async Task<ToDoResponseDto> ChangePriorityAsync(ChangeToDoPriorityDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangePriority, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>()
                      ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.ChangePriority}");
        }

        public async Task<ToDoResponseDto> ChangeStatusAsync(ChangeToDoStatusDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangeStatus, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>()
                      ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.ChangeStatus}");
        }

        public async Task<ToDoResponseDto> CreateAsync(CreateToDoDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.Create, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>()
                      ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.Create}");
        }

        public async Task<ToDoResponseDto> DeleteAsync(DeleteToDoDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.Delete, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>()
                   ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.Delete}");
        }
    }
}
