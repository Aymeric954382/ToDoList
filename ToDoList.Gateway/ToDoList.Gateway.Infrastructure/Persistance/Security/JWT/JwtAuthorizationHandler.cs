using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.Providers;

namespace ToDoList.Gateway.Infrastructure.Persistance.Security.JWT
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly IInternalJwtTokenProvider _tokenProvider;

        public JwtAuthorizationHandler(IInternalJwtTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenProvider.GetToken());
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
