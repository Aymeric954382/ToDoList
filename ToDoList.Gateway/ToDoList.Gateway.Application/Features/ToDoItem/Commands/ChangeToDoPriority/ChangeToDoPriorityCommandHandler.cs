using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommandHandler 
        : IRequestHandler<ChangeToDoPriorityCommand, 
                ServiceResult<TaskStateServiceChangePriorityResponseDto>>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        public ChangeToDoPriorityCommandHandler(ITaskStateServiceApiClientAdapter clientAdapter)
        {
            _clientAdapter = clientAdapter;
        }
        public async Task<ServiceResult<TaskStateServiceChangePriorityResponseDto>> Handle(ChangeToDoPriorityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceResult = await _clientAdapter.ChangePriorityAsync(request, cancellationToken);

                if (!serviceResult.ExecutionSuccess)
                    return ServiceResult<TaskStateServiceChangePriorityResponseDto>.Fail(serviceResult.Error ?? ServiceErrorCode.Unknown);

                return ServiceResult<TaskStateServiceChangePriorityResponseDto>.VoidDataSuccess();
            }
            catch (HttpRequestException)
            {
                return ServiceResult<TaskStateServiceChangePriorityResponseDto>.Fail(ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //throw new UnknownException(ex, request.Id); replace to logger

                return ServiceResult<TaskStateServiceChangePriorityResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
