using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Common.Exceptions
{
    public abstract class ApiException : Exception
    {
        protected ApiException(string message) : base(message) { }
    }
}
