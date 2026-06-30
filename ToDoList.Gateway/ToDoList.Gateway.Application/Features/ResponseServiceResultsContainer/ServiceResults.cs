using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces;

namespace ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer
{
    public abstract class ServiceResult
    {
        public bool ExecutionSuccess { get; init; }
        public ServiceErrorCode Error { get; init; }
        public DateTime ResponseDate { get; } = DateTime.UtcNow;
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

        public static ServiceResult<T> VoidDataSuccess()
        {
            return new ServiceResult<T>
            {
                ExecutionSuccess = true,
            };
        }

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