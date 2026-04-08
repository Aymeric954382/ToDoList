using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;

namespace ToDoList.TaskStateService.Application.Interfaces
{
    public interface IServiceResult
    {
        bool ExecutionSuccess { get; init; }
        ServiceErrorCode Error { get; init; }
        DateTime ResponseDate { get; }

        object Data { get; }
        static string GetErrorMessage(ServiceErrorCode errorCode) =>
            ServiceErrorCodeMessages.GetMessage(errorCode);
    }
}
