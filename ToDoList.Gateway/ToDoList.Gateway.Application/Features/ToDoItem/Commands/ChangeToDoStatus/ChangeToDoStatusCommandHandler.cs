using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommandHandler
        : IRequestHandler<
            ChangeToDoStatusCommand,
            ServiceResult<TaskStateServiceChangeStatusResponseDto>>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        private readonly ILogger _logger;

        public ChangeToDoStatusCommandHandler(
            ITaskStateServiceApiClientAdapter clientAdapter,
            ILogger logger)
        {
            _clientAdapter = clientAdapter;
            _logger = logger;
        }

        public async Task<ServiceResult<TaskStateServiceChangeStatusResponseDto>> Handle(
            ChangeToDoStatusCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "ChangeToDoStatus started. TaskId={TaskId}, UserId={UserId}, Status={Status}",
                request.Id,
                request.UserId,
                request.Status);

            try
            {
                await _clientAdapter.ChangeStatusAsync(
                    request,
                    cancellationToken);

                _logger.Information(
                    "ChangeToDoStatus completed successfully. TaskId={TaskId}",
                    request.Id);

                return ServiceResult<TaskStateServiceChangeStatusResponseDto>
                    .VoidDataSuccess();
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "ChangeToDoStatus failed. TaskId={TaskId}, UserId={UserId}",
                    request.Id,
                    request.UserId);

                return ServiceResult<TaskStateServiceChangeStatusResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}