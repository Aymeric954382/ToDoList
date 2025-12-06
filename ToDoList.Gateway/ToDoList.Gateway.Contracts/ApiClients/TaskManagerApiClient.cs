using System.Net.Http.Headers;
using System.Net.Http.Json;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
using ToDoList.Gateway.Contracts.Providers;

namespace ToDoList.Gateway.Contracts.ApiClients
{
    public class TaskManagerApiClient : ITaskManagerApiClient
    {
        private readonly HttpClient _http;
        private readonly IInternalJwtTokenProvider _tokenProvider;

        public TaskManagerApiClient(HttpClient http, IInternalJwtTokenProvider tokenProvider)
        {
            _http = http;
            _tokenProvider = tokenProvider;
        }

        public async Task<ToDoResponseDto> ChangeContentAsync(ChangeToDoContentDto dto)
        {
            var response = await _http.PostAsJsonAsync("", dto);
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>();
        }

        public async Task<ToDoResponseDto> CreateAsync(CreateToDoDto dto)
        {
            var response = await _http.PostAsJsonAsync("", dto);
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>();
        }

        public async Task<ToDoResponseDto> DeleteAsync(DeleteToDoDto dto)
        {
            var response = await _http.PostAsJsonAsync("", dto);
            return await response.Content.ReadFromJsonAsync<ToDoResponseDto>();
        }
    }

}
