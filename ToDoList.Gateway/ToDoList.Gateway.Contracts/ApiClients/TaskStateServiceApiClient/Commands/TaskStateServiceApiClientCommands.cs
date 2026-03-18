using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;


namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Commands
{
    public class TaskStateServiceApiClientCommands : ITaskStateServiceApiClientCommands
    {
        private readonly HttpClient _http;
        private readonly TaskStateServiceApiOptions _options;

        public TaskStateServiceApiClientCommands(HttpClient http, IOptions<TaskStateServiceApiOptions> options)
        {
            _http = http;
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (_options.Routes == null)
            {
                throw new InvalidOperationException(
                $"TaskStateServiceApiOptions.Routes is null. Called from " +
                $"{nameof(TaskStateServiceApiClientCommands)} constructor."
                );
            }
        }
        public async Task<TaskStateServiceChangeDueDateResponseDto> ChangeDueDateAsync(TaskStateServiceChangeDueDateRequestDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangeDueDate, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskStateServiceChangeDueDateResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.ChangeDueDate}");
        }

        public async Task<TaskStateServiceChangePriorityResponseDto> ChangePriorityAsync(TaskStateServiceChangePriorityRequestDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangePriority, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskStateServiceChangePriorityResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.ChangePriority}");
        }

        public async Task<TaskStateServiceChangeStatusResponseDto> ChangeStatusAsync(TaskStateServiceChangeStatusRequestDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangeStatus, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskStateServiceChangeStatusResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.ChangeStatus}");
        }

        public async Task<TaskStateServiceCreateResponseDto> CreateAsync(TaskStateServiceCreateRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.Create, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskStateServiceCreateResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.Create}");
        }

        public async Task<TaskStateServiceDeleteResponseDto> DeleteAsync(TaskStateServiceDeleteRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.Delete, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskStateServiceDeleteResponseDto>()
                ?? throw new InvalidOperationException($"Response body was null from {_options.Routes.Delete}");
        }
    }
}
