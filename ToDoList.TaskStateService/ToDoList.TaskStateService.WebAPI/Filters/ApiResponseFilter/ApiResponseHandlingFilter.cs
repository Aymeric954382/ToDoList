using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces;

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

                objectResult.Value = new
                {
                    success = result.ExecutionSuccess,
                    code = result.Error,
                    message = IServiceResult.GetErrorMessage(result.Error),
                    data = result.Data
                };
            }

            return;
        }
    }
}
