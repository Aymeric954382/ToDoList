using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.ServiceQueries;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Queries
{
    public class TaskStateServiceApiClientQueries : ITaskStateServiceApiClientQueries
    {
        private readonly HttpClient _http;
        private readonly TaskStateServiceApiOptions _options;
        public TaskStateServiceApiClientQueries(HttpClient http, TaskStateServiceApiOptions options)
        {
            _http = http;
            _options = options;
        }
        public async Task<TaskStateServiceGetToDoListByPriorityResponseDto> GetToDoListAsync(TaskStateServiceGetToDoListRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoList, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<TaskStateServiceGetToDoListByPriorityResponseDto>();

            return items;
        }

        public async Task<TaskStateServiceGetToDoListByIdsResponseDto> GetToDoListByIdAsync(TaskStateServiceGetToDoListByIdsRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoByIdList, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<TaskStateServiceGetToDoListByIdsResponseDto>();

            return items;
        }

        public async Task<TaskStateServiceGetToDoListByOverdueResponseDto> GetToDoListByOverdueAsync(TaskStateServiceGetToDoListByOverdueRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoListByOverdue, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<TaskStateServiceGetToDoListByOverdueResponseDto>();

            return items;
        }

        public async Task<TaskStateServiceGetToDoListByPriorityResponseDto> GetToDoListByPriorityAsync(GetToDoListByPriorityRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoListByPriority, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<TaskStateServiceGetToDoListByPriorityResponseDto>();

            return items;
        }

        public async Task<TaskStateServiceGetToDoListByStatusResponseDto> GetToDoListByStatusAsync(TaskStateServiceGetToDoListByStatusRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoListByStatus, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<TaskStateServiceGetToDoListByStatusResponseDto>();

            return items;
        }
    }
}
