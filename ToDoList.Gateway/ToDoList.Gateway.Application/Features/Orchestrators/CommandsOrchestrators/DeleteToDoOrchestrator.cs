using Serilog;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;

namespace ToDoList.Gateway.Application.Features.Orchestrators.CommandsOrchestrators
{
    public class DeleteToDoOrchestrator : IDeleteToDoOrchestrator
    {
        private readonly ITaskStateServiceApiClientAdapter _serviceApiClient;
        private readonly ITaskManagerApiClientAdapter _managerApiClient;
        private readonly ILogger _logger;

        public DeleteToDoOrchestrator(
            ITaskStateServiceApiClientAdapter serviceApiClient,
            ITaskManagerApiClientAdapter managerApiClient,
            ILogger logger)
        {
            _serviceApiClient = serviceApiClient;
            _managerApiClient = managerApiClient;
            _logger = logger;
        }

        public async Task<ServiceResult<DeleteToDoResponseDto>> DeleteAsync(
            DeleteToDoCommand command,
            CancellationToken cancellationToken)
        {
            _logger.Information("DeleteToDo orchestration started");

            try
            {
                await _managerApiClient.DeleteAsync(command, cancellationToken);
                _logger.Information("TaskManager delete completed");

                await _serviceApiClient.DeleteAsync(command, cancellationToken);
                _logger.Information("TaskStateService delete completed");

                return ServiceResult<DeleteToDoResponseDto>
                    .VoidDataSuccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "DeleteToDo orchestration failed");

                return ServiceResult<DeleteToDoResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}