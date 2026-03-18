using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;

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

        public async Task<TaskManagerChangeContentResponseDto> ChangeContentAsync(TaskManagerChangeContentRequestDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangeContent, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskManagerChangeContentResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.ChangeContent}");
        }

        public async Task<TaskManagerDeleteResponseDto> DeleteAsync(TaskManagerDeleteRequestDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.Delete, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskManagerDeleteResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.Delete}");

        }

        public async Task<TaskManagerCreateResponseDto> CreateAsync(TaskManagerCreateRequestDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.Create, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskManagerCreateResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.Create}");
        }
    }

}
