using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;

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
        public async Task<ToDoListContainerDto> GetToDoListAsync(GetToDoListDto dto)
        {
            var response = await _http.PostAsJsonAsync(_options.Routes.GetToDoList,dto);
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<ToDoListContainerDto>();

            return items;
        }
    }
}
