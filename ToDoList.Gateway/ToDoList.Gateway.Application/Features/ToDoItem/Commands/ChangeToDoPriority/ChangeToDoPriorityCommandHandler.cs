using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoPriority;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommandHandler
        : IRequestHandler<
            ChangeToDoPriorityCommand,
            ServiceResult<TaskStateServiceChangePriorityResponseDto>>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        private readonly ILogger _logger;

        public ChangeToDoPriorityCommandHandler(
            ITaskStateServiceApiClientAdapter clientAdapter,
            ILogger logger)
        {
            _clientAdapter = clientAdapter;
            _logger = logger;
        }

        public async Task<ServiceResult<TaskStateServiceChangePriorityResponseDto>> Handle(
            ChangeToDoPriorityCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "ChangeToDoPriority started. TaskId={TaskId}, UserId={UserId}, Priority={Priority}",
                request.Id,
                request.UserId,
                request.Priority);

            try
            {
                await _clientAdapter.ChangePriorityAsync(
                    request,
                    cancellationToken);

                _logger.Information(
                    "ChangeToDoPriority completed successfully. TaskId={TaskId}",
                    request.Id);

                return ServiceResult<TaskStateServiceChangePriorityResponseDto>
                    .VoidDataSuccess();
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "ChangeToDoPriority failed. TaskId={TaskId}, UserId={UserId}",
                    request.Id,
                    request.UserId);

                return ServiceResult<TaskStateServiceChangePriorityResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}