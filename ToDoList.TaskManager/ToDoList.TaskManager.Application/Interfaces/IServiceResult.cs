using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;

namespace ToDoList.TaskManager.Application.Interfaces
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
