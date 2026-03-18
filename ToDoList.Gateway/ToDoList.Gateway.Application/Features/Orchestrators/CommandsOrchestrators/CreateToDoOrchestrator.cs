using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;

namespace ToDoList.Gateway.Application.Features.Orchestrators.CommandsOrchestrators
{
    public class CreateToDoOrchestrator : ICreateToDoOrchestrator
    {
        private readonly ITaskStateServiceApiClientAdapter _serviceApiClient;
        private readonly ITaskManagerApiClientAdapter _managerApiClient;
        public CreateToDoOrchestrator(ITaskStateServiceApiClientAdapter serviceApiClient, 
            ITaskManagerApiClientAdapter managerApiClient)
        {
            _serviceApiClient = serviceApiClient;
            _managerApiClient = managerApiClient;
        }
        public async Task<ServiceResult<CreateToDoResponseDto>> CreateAsync(
            CreateToDoCommand command,
            CancellationToken cancellationToken)
        {
            var managerResult = await _managerApiClient.CreateAsync(command, cancellationToken);

            if (!managerResult.ExecutionSuccess)
                return ServiceResult<CreateToDoResponseDto>.Fail(
                    managerResult.Error ?? ServiceErrorCode.Unknown);

            try
            {
                var serviceResult = await _serviceApiClient.CreateAsync(
                    command,
                    managerResult.Data.Id,
                    cancellationToken);

                if (!serviceResult.ExecutionSuccess)
                {
                    return ServiceResult<CreateToDoResponseDto>.Fail(
                        serviceResult.Error ?? ServiceErrorCode.Unknown);
                }

                return ServiceResult<CreateToDoResponseDto>.Success(
                    new CreateToDoResponseDto
                    {
                        Id = managerResult.Data.Id
                    });
            }
            catch (HttpRequestException)
            {
                return ServiceResult<CreateToDoResponseDto>.Fail(ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                try
                {
                    //await _managerApiClient.DeleteAsync(managerResult.Data.Id, cancellationToken); need service command DeleteById
                }
                catch (Exception rollbackEx)
                {
                    //rollback failure (logger)
                }

                //throw new UnknownException(ex, request.Id); replace to logger

                return ServiceResult<CreateToDoResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
