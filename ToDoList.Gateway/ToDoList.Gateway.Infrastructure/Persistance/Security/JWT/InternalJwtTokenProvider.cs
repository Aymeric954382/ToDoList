using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Infrastructure.Persistance.Security.JWT
{
    public class InternalJwtTokenProvider
    {
        private readonly InternalJwtTokenGenerator _generator;

        public InternalJwtTokenProvider(InternalJwtTokenGenerator generator)
        {
            _generator = generator;
        }

        public string GetToken(IEnumerable<Claim> claims)
        {
            return _generator.Generate(claims).Token;
        }
    }

}
