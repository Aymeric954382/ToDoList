using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public string ServiceName { get; }
        public string Operation { get; }

        public ExternalServiceException(
            string serviceName,
            string operation,
            Exception? innerException = null)
            : base($"Error calling '{operation}' on '{serviceName}'", innerException)
        {
            ServiceName = serviceName;
            Operation = operation;
        }
    }
}
