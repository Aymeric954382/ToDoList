using Serilog;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.CreateToDo;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.Exceptions;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Workflows
{
    public class CreateToDoWorkflow : ICreateToDoWorkflow
    {
        private readonly ITaskStateServiceApiClientAdapter _serviceApiClient;
        private readonly ITaskManagerApiClientAdapter _managerApiClient;
        private readonly ILogger _logger;

        private readonly Guid id = new Guid();

        public CreateToDoWorkflow(
            ITaskStateServiceApiClientAdapter serviceApiClient,
            ITaskManagerApiClientAdapter managerApiClient,
            ILogger logger)
        {
            _serviceApiClient = serviceApiClient;
            _managerApiClient = managerApiClient;
            _logger = logger;
        }

        public async Task CreateAsync(
            CreateToDoCommand command,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "CreateToDo orchestration started. UserId={UserId}",
                command.UserId);
            
            //manager public 
            try
            {
                await _managerApiClient.CreateAsync(
                    command,
                    id,
                    cancellationToken); 

                _logger.Information(
                    "TaskManager create succeeded. TaskId={TaskId}",
                    id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex,
                    "TaskManager create failed. TaskId={TaskId}",
                    id);
                
                throw new ExternalServiceException(
                    "TaskStateService",
                    "CreateToDo",
                    ex);
            }
            
            //state public
            try
            {
                await _serviceApiClient.CreateAsync(
                    command,
                    id,
                    cancellationToken);

                _logger.Information(
                    "TaskStateService create succeeded. TaskId={TaskId}",
                    id);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "TaskStateService create failed. TaskId={TaskId}",
                    id);
                
                throw new ExternalServiceException(
                    "TaskStateService",
                    "CreateToDo",
                    ex);
            }
        }
    }
}