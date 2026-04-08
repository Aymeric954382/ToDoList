namespace ToDoList.TaskStateService.WebAPI.Middlewares
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
