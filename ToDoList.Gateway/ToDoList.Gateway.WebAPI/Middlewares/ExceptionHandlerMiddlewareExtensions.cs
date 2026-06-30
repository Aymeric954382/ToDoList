using ToDoList.Gateway.WebAPI.Middlewares;

namespace ToDoList.TaskStateService.WebAPI.Middlewares;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}
