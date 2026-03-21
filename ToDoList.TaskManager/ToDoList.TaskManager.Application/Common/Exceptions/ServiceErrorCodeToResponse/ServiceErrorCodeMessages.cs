using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse
{
    public static class ServiceErrorCodeMessages
    {
        private static readonly Dictionary<ServiceErrorCode, string> _messages = new()
        {
            { ServiceErrorCode.Unknown, "An unexpected error occurred." },
            { ServiceErrorCode.NotFound, "Requested item was not found." },
            { ServiceErrorCode.ValidationFailed, "Input data is invalid." },
            { ServiceErrorCode.Conflict, "Item already exists or conflicts with existing data." },
            { ServiceErrorCode.ServiceUnavailable, "Service temporarily unavailable. Please try again later." }
        };

        public static string GetMessage(ServiceErrorCode code)
       => _messages.TryGetValue(code, out var msg) ? msg : _messages[ServiceErrorCode.Unknown];
    }
}
