namespace ToDoList.TaskManager.WebAPI.Middlewares
{
    public static class ExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
