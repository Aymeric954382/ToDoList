using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.Providers
{
    public interface IInternalJwtTokenProvider
    {
        string GetToken();
    }
}
