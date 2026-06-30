using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.Exceptions;

namespace ToDoList.Gateway.Application.Features.Orchestrators.CommandsOrchestrators
{
    public class CreateToDoOrchestrator : ICreateToDoOrchestrator
    {
        private readonly ITaskStateServiceApiClientAdapter _serviceApiClient;
        private readonly ITaskManagerApiClientAdapter _managerApiClient;
        private readonly ILogger _logger;

        public CreateToDoOrchestrator(
            ITaskStateServiceApiClientAdapter serviceApiClient,
            ITaskManagerApiClientAdapter managerApiClient,
            ILogger logger)
        {
            _serviceApiClient = serviceApiClient;
            _managerApiClient = managerApiClient;
            _logger = logger;
        }

        public async Task<ServiceResult<CreateToDoResponseDto>> CreateAsync(
            CreateToDoCommand command,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "CreateToDo orchestration started. UserId={UserId}",
                command.UserId);

            var managerResult = await _managerApiClient.CreateAsync(
                command,
                cancellationToken);

            if (managerResult == null)
            {
                _logger.Error(
                    "TaskManager returned null result. UserId={UserId}",
                    command.UserId);

                return ServiceResult<CreateToDoResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }

            _logger.Information(
                "TaskManager create succeeded. TaskId={TaskId}",
                managerResult.Id);

            try
            {
                await _serviceApiClient.CreateAsync(
                    command,
                    managerResult.Id,
                    cancellationToken);

                _logger.Information(
                    "TaskStateService create succeeded. TaskId={TaskId}",
                    managerResult.Id);

                return ServiceResult<CreateToDoResponseDto>.Success(
                    new CreateToDoResponseDto
                    {
                        Id = managerResult.Id
                    });
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "TaskStateService create failed. TaskId={TaskId}",
                    managerResult.Id);

                // rollback (best-effort)
                try
                {
                    //await _managerApiClient.DeleteAsync(
                    //    managerResult.Id,
                    //    cancellationToken);

                    _logger.Warning(
                        "Rollback succeeded. TaskId={TaskId}",
                        managerResult.Id);
                }
                catch (Exception rollbackEx)
                {
                    _logger.Error(
                        rollbackEx,
                        "Rollback failed. TaskId={TaskId}",
                        managerResult.Id);
                }

                throw new ExternalServiceException(
                    "TaskStateService",
                    "CreateToDo",
                    ex);
            }
        }
    }
}