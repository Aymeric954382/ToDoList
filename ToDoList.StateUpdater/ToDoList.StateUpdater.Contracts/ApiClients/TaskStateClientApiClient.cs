using ToDoList.StateUpdater.Contracts.ApiClients.Interfaces;
using ToDoList.StateUpdater.Contracts.ApiClients.RequestDto;
using System.Net.Http.Json;

namespace ToDoList.StateUpdater.Contracts.ApiClients
{
    public class TaskStateClientApiClient : ITaskStateClientApiClient
    {
        private readonly HttpClient _http;
        public TaskStateClientApiClient(HttpClient http)
        {
            _http = http;
        }
        public async Task UpdateDeadLines(UpdateToDoDeadLinesRequestDto dto, CancellationToken cancellationToken)
        {
            await _http.PostAsJsonAsync("", dto);
        }
    }
}
