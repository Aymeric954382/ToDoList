using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse
{
    public enum ServiceErrorCode
    {
        Unknown,
        NotFound,
        ValidationFailed,
        Conflict,
        ServiceUnavailable
    }
}
