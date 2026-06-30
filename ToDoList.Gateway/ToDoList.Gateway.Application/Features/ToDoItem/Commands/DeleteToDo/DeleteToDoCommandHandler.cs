using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler
        : IRequestHandler<DeleteToDoCommand,
            ServiceResult<DeleteToDoResponseDto>>
    {
        private readonly IDeleteToDoOrchestrator _orchestrator;
        private readonly ILogger _logger;

        public DeleteToDoCommandHandler(
            IDeleteToDoOrchestrator orchestrator,
            ILogger logger)
        {
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task<ServiceResult<DeleteToDoResponseDto>> Handle(
            DeleteToDoCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information("DeleteToDo started");

            try
            {
                var result = await _orchestrator.DeleteAsync(request, cancellationToken);

                _logger.Information("DeleteToDo completed successfully");

                return result;
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