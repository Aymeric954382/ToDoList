using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;

namespace ToDoList.Gateway.Application.Interfaces
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
