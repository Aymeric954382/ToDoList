using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Queries
{
    public class TaskStateClientApiClientQueries : ITaskStateClientApiClientQueries
    {
        private readonly HttpClient _http;
        private readonly TaskStateServiceApiOptions _options;
        public TaskStateClientApiClientQueries(HttpClient http, TaskStateServiceApiOptions options)
        {
            _http = http;
            _options = options;
        }
        public async Task<ToDoListContainerDto> GetToDoListAsync(GetToDoListDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoList, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<ToDoListContainerDto>();

            return items;
        }

        public async Task<ToDoListContainerDto> GetToDoListByOverdueAsync(GetToDoListByOverdueDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoListByOverdue, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<ToDoListContainerDto>();

            return items;
        }

        public async Task<ToDoListContainerDto> GetToDoListByPriorityAsync(GetToDoListByPriorityDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoListByPriority, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<ToDoListContainerDto>();

            return items;
        }

        public async Task<ToDoListContainerDto> GetToDoListByStatusAsync(GetToDoListByStatusDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoListByStatus, dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<ToDoListContainerDto>();

            return items;
        }
    }
}
