using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces;

namespace ToDoList.Gateway.Infrastructure.Persistance.Security.JWT
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly InternalJwtTokenGenerator _generator;
        private readonly ICurrentUserService _currentUser;

        public JwtAuthorizationHandler(
            InternalJwtTokenGenerator generator,
            ICurrentUserService currentUser)
        {
            _generator = generator;
            _currentUser = currentUser;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var claims = _currentUser.Claims;

            var token = _generator.Generate(claims).Token;

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
