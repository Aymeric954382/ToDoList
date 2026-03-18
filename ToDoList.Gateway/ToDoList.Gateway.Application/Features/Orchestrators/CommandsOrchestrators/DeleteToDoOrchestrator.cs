using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;

namespace ToDoList.Gateway.Application.Features.Orchestrators.CommandsOrchestrators
{
    public class DeleteToDoOrchestrator : IDeleteToDoOrchestrator
    {
        private readonly ITaskStateServiceApiClientAdapter _serviceApiClient;
        private readonly ITaskManagerApiClientAdapter _managerApiClient;
        public DeleteToDoOrchestrator(ITaskStateServiceApiClientAdapter serviceApiClient, 
            ITaskManagerApiClientAdapter managerApiClient)
        {
            _serviceApiClient = serviceApiClient;
            _managerApiClient = managerApiClient;
        }
        public async Task<ServiceResult<DeleteToDoResponseDto>> DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var managerResult = await _managerApiClient.DeleteAsync(command, cancellationToken);

            if (!managerResult.ExecutionSuccess)
                return ServiceResult<DeleteToDoResponseDto>.Fail(
                    managerResult.Error ?? ServiceErrorCode.Unknown);

            try
            {
                var serviceResult = await _serviceApiClient.DeleteAsync(command, cancellationToken);

                if (!serviceResult.ExecutionSuccess)
                {
                    return ServiceResult<DeleteToDoResponseDto>.Fail(serviceResult.Error ?? ServiceErrorCode.Unknown);
                }

                return ServiceResult<DeleteToDoResponseDto>.VoidDataSuccess();
            }
            catch (HttpRequestException)
            {
                return ServiceResult<DeleteToDoResponseDto>.Fail(ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //logger 

                return ServiceResult<DeleteToDoResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
