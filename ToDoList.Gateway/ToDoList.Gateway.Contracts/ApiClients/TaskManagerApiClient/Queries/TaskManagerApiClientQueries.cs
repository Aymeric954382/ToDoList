using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.ServiceQueries;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Queries
{
    public class TaskManagerApiClientQueries : ITaskManagerApiClientQueries
    {
        private readonly HttpClient _http;
        private readonly TaskManagerApiOptions _options;
        public TaskManagerApiClientQueries(HttpClient http, TaskManagerApiOptions options)
        {
            _http = http;
            _options = options;

        }
        public async Task<TaskManagerGetToDoListResponseDto> GetToDoListAsync(TaskManagerGetToDoListRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoList,dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<TaskManagerGetToDoListResponseDto>();

            return items;
        }

        public async Task<TaskManagerGetToDoListByIdsResponseDto> GetToDoListByIdAsync(TaskManagerGetToDoListByIdsRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoByIdList, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<TaskManagerGetToDoListByIdsResponseDto>();

            return items;
        }
    }
}
