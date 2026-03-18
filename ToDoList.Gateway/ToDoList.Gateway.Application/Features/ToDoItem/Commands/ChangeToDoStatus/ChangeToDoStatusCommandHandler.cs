using MediatR;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommandHandler 
        : IRequestHandler<ChangeToDoStatusCommand, 
            ServiceResult<TaskStateServiceChangeStatusResponseDto>>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        public ChangeToDoStatusCommandHandler(ITaskStateServiceApiClientAdapter clientAdapter)
        {
            _clientAdapter = clientAdapter;
        }
        public async Task<ServiceResult<TaskStateServiceChangeStatusResponseDto>> Handle(ChangeToDoStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceResult = await _clientAdapter.ChangeStatusAsync(request, cancellationToken);

                if (!serviceResult.ExecutionSuccess)
                    return ServiceResult<TaskStateServiceChangeStatusResponseDto>.Fail(serviceResult.Error ?? ServiceErrorCode.Unknown);

                return ServiceResult<TaskStateServiceChangeStatusResponseDto>.VoidDataSuccess();
            }
            catch (HttpRequestException)
            {
                return ServiceResult<TaskStateServiceChangeStatusResponseDto>.Fail(ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //throw new UnknownException(ex, request.Id); replace to logger

                return ServiceResult<TaskStateServiceChangeStatusResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
