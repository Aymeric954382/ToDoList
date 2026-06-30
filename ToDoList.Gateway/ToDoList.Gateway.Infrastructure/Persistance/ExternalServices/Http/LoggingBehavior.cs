using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Http
{
    public class LoggingHandler : DelegatingHandler
    {
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

            Log.Information(
                "HTTP call. Method: {Method}," +
                " Url: {Url}, " +
                "Status: {StatusCode}, " +
                "Service: {Service}, " +
                "Operation: {Operation}",
                request.Method,
                request.RequestUri,
                response.StatusCode,
                service,
                operation);

            return response;
        }
    }
}
