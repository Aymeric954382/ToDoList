using FluentValidation;
using System.Net;
using System.Text.Json;
using ToDoList.Application.Common.Exceptions;

namespace ToDoList.WebAPI.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        public readonly RequestDelegate _next;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }

        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            object result = null;

            switch (exception)
            {
                case ValidationException validationExcteption:
                    code = HttpStatusCode.BadRequest;
                    result = validationExcteption.Errors;
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                default:
                    result = new { error = exception.Message };
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            ;
            var serializedResult = JsonSerializer.Serialize(result, _jsonOptions);

            return context.Response.WriteAsync(serializedResult);
        }
    }
}
