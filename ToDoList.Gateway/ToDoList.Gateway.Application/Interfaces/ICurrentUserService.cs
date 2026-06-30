using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Interfaces
{
    public interface ICurrentUserService
    {
        public IEnumerable<Claim> Claims { get; }
    }
}
