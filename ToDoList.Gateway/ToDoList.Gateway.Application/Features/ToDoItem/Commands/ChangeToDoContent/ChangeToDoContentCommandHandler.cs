using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoContent;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommandHandler
        : IRequestHandler<
            ChangeToDoContentCommand,
            ServiceResult<TaskStateServiceChangeContentResponseDto>>
    {
        private readonly ITaskManagerApiClientAdapter _clientAdapter;
        private readonly ILogger _logger;

        public ChangeToDoContentCommandHandler(
            ITaskManagerApiClientAdapter clientAdapter,
            ILogger logger)
        {
            _clientAdapter = clientAdapter;
            _logger = logger;
        }

        public async Task<ServiceResult<TaskStateServiceChangeContentResponseDto>> Handle(
            ChangeToDoContentCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information("ChangeToDoContent started");

            try
            {
                await _clientAdapter.ChangeContentAsync(
                    request,
                    cancellationToken);

                _logger.Information("ChangeToDoContent completed successfully");

                return ServiceResult<TaskStateServiceChangeContentResponseDto>
                    .VoidDataSuccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "ChangeToDoContent failed");

                return ServiceResult<TaskStateServiceChangeContentResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}