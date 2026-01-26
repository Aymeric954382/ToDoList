using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;


namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient
{
    public class TaskStateServiceApiClient : ITaskStateServiceApiClient
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

        public async Task<HttpResponseMessage> ChangeDueDateAsync(ChangeToDoDueDateDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangeDueDate, dto);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> ChangePriorityAsync(ChangeToDoPriorityDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangePriority, dto);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> ChangeStatusAsync(ChangeToDoStatusDto dto)
        {
            var response = await _http.PatchAsJsonAsync(_options.Routes.ChangeStatus, dto);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateForServiceToDoDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.Create, dto);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(DeleteToDoDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.Delete, dto);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
