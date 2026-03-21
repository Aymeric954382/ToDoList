using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;

namespace ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer
{
    public abstract class ServiceResult
    {
        public bool ExecutionSuccess { get; init; }
        public ServiceErrorCode? Error { get; init; }
        public DateTime ResponseDate { get; } = DateTime.UtcNow;
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; init; }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>
            {
                Data = data,
                ExecutionSuccess = true,
            };
        }

        public static ServiceResult<T> VoidDataSuccess()
        {
            return new ServiceResult<T>
            {
                ExecutionSuccess = true,
            };
        }

        public string GetErrorMessage() => Error.HasValue
            ? ServiceErrorCodeMessages.GetMessage(Error.Value)
            : null;

        public static ServiceResult<T> Fail(ServiceErrorCode error)
        {
            return new ServiceResult<T>
            {
                ExecutionSuccess = false,
                Error = error,
                Data = default
            };
        }
    }
}

