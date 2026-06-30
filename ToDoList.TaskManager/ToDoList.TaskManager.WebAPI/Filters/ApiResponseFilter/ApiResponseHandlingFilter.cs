using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Interfaces;
using ToDoList.TaskManager.WebAPI.Models;

namespace ToDoList.TaskStateService.WebAPI.Filters.ApiResponseFilter
{
    public class ApiResponseHandlingFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext executedContext = await next();

            if (executedContext.Result is ObjectResult objectResult &&
                objectResult.Value is IServiceResult result)
            {
                objectResult.StatusCode = result.ExecutionSuccess
                    ? StatusCodes.Status200OK
                    : result.Error switch
                    {
                        ServiceErrorCode.NotFound => StatusCodes.Status404NotFound,
                        ServiceErrorCode.Conflict => StatusCodes.Status409Conflict,
                        ServiceErrorCode.ServiceUnavailable => StatusCodes.Status503ServiceUnavailable,
                        ServiceErrorCode.Unknown => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status400BadRequest
                    };

                objectResult.Value = new ApiResponse<object>
                {
                    ExecutionSuccess = result.ExecutionSuccess,
                    Code = result.Error,
                    Message = IServiceResult.GetErrorMessage(result.Error),
                    Data = result.Data
                };
            }

            return;
        }
    }
}
