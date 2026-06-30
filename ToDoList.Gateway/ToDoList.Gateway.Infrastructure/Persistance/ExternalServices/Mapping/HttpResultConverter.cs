using AutoMapper;
using System.Text.Json;
using System.Xml.Linq;
using ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Model;

namespace ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Mapping
{
    public class HttpResultConverter
    {
        private readonly IMapper _mapper;
        public HttpResultConverter(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ExternalServiceResult> Convert(HttpResponseMessage response)
        {
            string? message = null;

            var json = await response.Content.ReadAsStringAsync();

            var element = JsonDocument.Parse(json).RootElement;

            if (element.ValueKind == JsonValueKind.Object &&
                element.TryGetProperty("Message", out var msg))
            {
                message = msg.GetString();
            }

            return new ExternalServiceResult
            {
                Message = message,
                Code = (int)response.StatusCode,
                ExecutionSuccess = response.IsSuccessStatusCode
            };
        }
    }
}
