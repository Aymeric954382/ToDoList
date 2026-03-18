using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Common.Exceptions
{
    public class UnknownException : Exception
    {
        public UnknownException(Exception exception, object key)
            : base($"Unknown error, on key - {key}, error - {exception}") { }
    }
}
