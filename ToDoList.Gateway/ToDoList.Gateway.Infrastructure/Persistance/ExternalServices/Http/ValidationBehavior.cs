using ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Http.CustomRulePipe;
using ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Mapping;
using ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Model;

namespace ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Http
{
    public class ValidationBehavior : DelegatingHandler
    {
        private readonly HttpResultConverter _converter;
        public ValidationBehavior(HttpResultConverter converter)
        {
            _converter = converter;
        }
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            var operationKey = new HttpRequestOptionsKey<string>("operation");

            var serviceKey = new HttpRequestOptionsKey<string>("service");

            if (!request.Options.TryGetValue(operationKey, out var operation))
            {
                operation = "unknown";
            }
            if (!request.Options.TryGetValue(serviceKey, out var service))
            {
                service = "unknown";
            }

            var externalResult = await _converter.Convert(response);

            var builder = Rule.MakeFor(
                externalResult,
                x => x.ExecutionSuccess
            ).NotNull();

            builder.ThrowIfInvalid(operation, service);

            return response;
        }
    }
}
