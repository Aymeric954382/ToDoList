using MediatR;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Common.Exceptions;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommandHandler 
        : IRequestHandler<ChangeToDoContentCommand, 
            ServiceResult<TaskStateServiceChangeContentResponseDto>>
    {
        private readonly ITaskManagerApiClientAdapter _clientAdapter;
        public ChangeToDoContentCommandHandler(ITaskManagerApiClientAdapter clientAdapter)
        {
            _clientAdapter = clientAdapter;
        }
        public async Task<ServiceResult<TaskStateServiceChangeContentResponseDto>> Handle(ChangeToDoContentCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var managerResult = await _clientAdapter.ChangeContentAsync(request, cancellationToken);

                if (!managerResult.ExecutionSuccess)
                    return ServiceResult<TaskStateServiceChangeContentResponseDto>.Fail(
                        managerResult.Error ?? ServiceErrorCode.Unknown);

                return ServiceResult<TaskStateServiceChangeContentResponseDto>.VoidDataSuccess();
            }
            catch (HttpRequestException)
            {
                return ServiceResult<TaskStateServiceChangeContentResponseDto>.Fail(ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //throw new UnknownException(ex, request.Id); replace to logger

                return ServiceResult<TaskStateServiceChangeContentResponseDto>.Fail(ServiceErrorCode.Unknown);
            }      
        }
    }
}
