using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommandHandler
        : IRequestHandler<
            ChangeToDoDueDateCommand,
            ServiceResult<TaskStateServiceChangeDueDateResponseDto>>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        private readonly ILogger _logger;

        public ChangeToDoDueDateCommandHandler(
            ITaskStateServiceApiClientAdapter clientAdapter,
            ILogger logger)
        {
            _clientAdapter = clientAdapter;
            _logger = logger;
        }

        public async Task<ServiceResult<TaskStateServiceChangeDueDateResponseDto>> Handle(
            ChangeToDoDueDateCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information("ChangeToDoDueDate started");

            try
            {
                await _clientAdapter.ChangeDueDateAsync(
                    request,
                    cancellationToken);

                _logger.Information("ChangeToDoDueDate completed successfully");

                return ServiceResult<TaskStateServiceChangeDueDateResponseDto>
                    .VoidDataSuccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "ChangeToDoDueDate failed");

                return ServiceResult<TaskStateServiceChangeDueDateResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}