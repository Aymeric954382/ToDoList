using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommandHandler 
        : IRequestHandler<ChangeToDoDueDateCommand, 
            ServiceResult<TaskStateServiceChangeDueDateResponseDto>>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        public ChangeToDoDueDateCommandHandler(ITaskStateServiceApiClientAdapter clientAdapter)
        {
            _clientAdapter = clientAdapter;
        }

        public async Task<ServiceResult<TaskStateServiceChangeDueDateResponseDto>> Handle(ChangeToDoDueDateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceResult = await _clientAdapter.ChangeDueDateAsync(request, cancellationToken);

                if (!serviceResult.ExecutionSuccess)
                    return ServiceResult<TaskStateServiceChangeDueDateResponseDto>.Fail(serviceResult.Error ?? ServiceErrorCode.Unknown);

                return ServiceResult<TaskStateServiceChangeDueDateResponseDto>.VoidDataSuccess();

            }
            catch (HttpRequestException)
            {
                return ServiceResult<TaskStateServiceChangeDueDateResponseDto>.Fail(ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //throw new UnknownException(ex, request.Id); replace to logger

                return ServiceResult<TaskStateServiceChangeDueDateResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
