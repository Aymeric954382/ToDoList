using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Infrastructure.Helpers.RequestFactory
{
    public static class HttpRequestFactory
    {
        public static HttpRequestMessage Create<T>(
            HttpMethod method,
            string url,
            T? body = default,
            string? operation = null,
            string? service = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (body is not null)
            {
                request.Content = JsonContent.Create(body);
            }

            if (!string.IsNullOrWhiteSpace(operation))
            {
                request.Options.Set(new HttpRequestOptionsKey<string>("operation"), operation);
            }
            if (!string.IsNullOrWhiteSpace(service))
            {
                request.Options.Set(new HttpRequestOptionsKey<string>("service"), service);
            }

            return request;
        }
    }
}
