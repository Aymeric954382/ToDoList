using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Interfaces;

namespace ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer
{
    public abstract class ServiceResult
    {
        public bool ExecutionSuccess { get; init; }
        public ServiceErrorCode Error { get; init; }
        public DateTime ResponseDate { get; } = DateTime.UtcNow;
        public string Message { get; init; } = string.Empty;
        public static string GetErrorMessage(ServiceErrorCode errorCode) => 
            ServiceErrorCodeMessages.GetMessage(errorCode);
    }

    public class ServiceResult<T> : ServiceResult, IServiceResult
    {
        public T? Data { get; init; }

        object IServiceResult.Data => Data;

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>
            {
                Data = data,
                ExecutionSuccess = true,
            };
        }

        public static ServiceResult<T> Fail(ServiceErrorCode error)
        {
            return new ServiceResult<T>
            {
                ExecutionSuccess = false,
                Error = error,
                Data = default,
                Message = GetErrorMessage(error)
            };
        }
    }

    public class InternalServiceResult<T> : ServiceResult, IServiceResult
    {
        public T? Data { get; init; }

        object IServiceResult.Data => Data;
        public static InternalServiceResult<T> Success(T data)
        {
            return new InternalServiceResult<T>
            {
                Data = data,
                ExecutionSuccess = true,
            };
        }
        public static InternalServiceResult<T> Fail(ServiceErrorCode error)
        {
            return new InternalServiceResult<T>
            {
                ExecutionSuccess = false,
                Error = error,
                Data = default,
                Message = GetErrorMessage(error)
            };
        }
    }
}

