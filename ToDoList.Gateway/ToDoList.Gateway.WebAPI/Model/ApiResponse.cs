using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;

namespace ToDoList.Gateway.WebAPI.Model
{
    public class ApiResponse
    {
        public bool ExecutionSuccess { get; set; }
        public ServiceErrorCode? Code { get; set; }
        public string Message { get; set; } = default!;
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data) => new()
        {
            ExecutionSuccess = true,
            Data = data
        };
        public static ApiResponse<T> Fail(ServiceErrorCode code, string message) => new()
        {
            ExecutionSuccess = false,
            Code = code,
            Message = message
        };
    }
}
