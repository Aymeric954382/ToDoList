using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.Providers;

namespace ToDoList.Gateway.Infrastructure.Persistance.Security.JWT
{
    public class InternalJwtTokenProvider : IInternalJwtTokenProvider
    {
        private readonly InternalJwtTokenGenerator _generator;
        private string _cachedToken;
        private DateTime _expiresAt;

        public InternalJwtTokenProvider(InternalJwtTokenGenerator generator)
        {
            _generator = generator;
        }

        public string GetToken()
        {
            if (_cachedToken == null || DateTime.UtcNow >= _expiresAt)
            {
                var result = _generator.Generate();
                _cachedToken = result;
                _expiresAt = _generator.ExpirationTime;
            }

            return _cachedToken;
        }
    }

}
