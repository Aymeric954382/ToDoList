using MediatR;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority
{
    public class GetToDoListByPriorityQueryHandler 
        : IRequestHandler<GetToDoListByPriorityQuery,
            ServiceResult<GetToDoListByPriorityResponseDto>>
    {
        private readonly IGetToDoListByPriorityOrchestrator _orchestrator;
        public GetToDoListByPriorityQueryHandler(IGetToDoListByPriorityOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }
        public async Task<ServiceResult<GetToDoListByPriorityResponseDto>> Handle(
            GetToDoListByPriorityQuery request, 
                CancellationToken cancellationToken)
        {
            return await _orchestrator.GetListByPriorityAsync(request, cancellationToken);

        }
    }
}
