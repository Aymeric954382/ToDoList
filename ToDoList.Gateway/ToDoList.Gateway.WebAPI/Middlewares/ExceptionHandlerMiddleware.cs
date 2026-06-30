using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Interfaces;
using ToDoList.Gateway.WebAPI.Model;

namespace ToDoList.Gateway.WebAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (!context.Response.HasStarted)
                    await _next(context);
                else
                    return;
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var (statusCode, errorCode) = ex switch
            {
                ValidationException => (StatusCodes.Status400BadRequest, ServiceErrorCode.ValidationFailed),
                _ => (StatusCodes.Status500InternalServerError, ServiceErrorCode.Unknown)
            };

            var response = new ApiResponse()
            {
                ExecutionSuccess = false,
                Code = errorCode,
                Message = IServiceResult.GetErrorMessage(errorCode)
            };


            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(response)); ;

            return;
        }
    }
}
