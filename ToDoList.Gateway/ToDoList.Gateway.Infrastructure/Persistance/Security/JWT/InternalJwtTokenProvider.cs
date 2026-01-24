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
        private readonly object _lock = new();

        public InternalJwtTokenProvider(InternalJwtTokenGenerator generator)
        {
            _generator = generator;
        }

        public string GetToken()
        {
            lock (_lock)
            {
                if (_cachedToken == null || DateTime.UtcNow >= _expiresAt.AddSeconds(-30))
                {
                    var result = _generator.Generate();
                    _cachedToken = result.Token;
                    _expiresAt = result.ExpiredAt;
                }
            }

            return _cachedToken;
        }
    }

}
