using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;

namespace ToDoList.TaskStateService.WebAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsynс(HttpContext context)
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
            int statusCode = ex switch
            {
                ValidationException => 400,
                _ => 500
            };

            var response = new ObjectResult(new
            {
                success = false,
                message = statusCode
            })
            {
               StatusCode = statusCode
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(response)); ;

            return;
        }
    }
}
