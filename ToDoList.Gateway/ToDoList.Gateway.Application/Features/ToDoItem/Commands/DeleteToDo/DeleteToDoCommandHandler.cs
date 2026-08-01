using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.DeleteToDo;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler
        : IRequestHandler<DeleteToDoCommand,
            ServiceResult<DeleteToDoResponseDto>>
    {
        private readonly IDeleteToDoWorkflow _workflow;
        private readonly ILogger _logger;

        public DeleteToDoCommandHandler(
            IDeleteToDoWorkflow workflow,
            ILogger logger)
        {
            _workflow = workflow;
            _logger = logger;
        }

        public async Task<ServiceResult<DeleteToDoResponseDto>> Handle(
            DeleteToDoCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information("DeleteToDo started");

            try
            {
                await _workflow.DeleteAsync(request, cancellationToken);

                _logger.Information("DeleteToDo completed successfully");

                return ServiceResult<DeleteToDoResponseDto>
                    .VoidDataSuccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "DeleteToDo failed");

                return ServiceResult<DeleteToDoResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}