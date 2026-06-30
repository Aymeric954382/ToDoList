using System.Net.Http.Json;

namespace ToDoList.StateUpdater.Infrastructure.Helpers.HttpExecutor
{
    public class HttpExecutor
    {
        private readonly HttpClient _http;


        public HttpExecutor(HttpClient http)
        {
            _http = http;
        }

        public async Task<T> ExecuteAsync<T>(HttpRequestMessage request)
        {
            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>()
               ?? throw new InvalidOperationException(
                   $"Response body was null from {request.RequestUri}");
        }
    }
}
