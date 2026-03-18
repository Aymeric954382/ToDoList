using MediatR;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo
{
    public class GetToDoListByOverdueQueryHandler 
        : IRequestHandler<GetToDoListByOverdueQuery, 
            ServiceResult<GetToDoListByOverdueResponseDto>>
    {
        private readonly IGetToDoListByOverdueOrchestrator _orchestrator;
        public GetToDoListByOverdueQueryHandler(IGetToDoListByOverdueOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }
        public async Task<ServiceResult<GetToDoListByOverdueResponseDto>> Handle(
            GetToDoListByOverdueQuery request, 
            CancellationToken cancellationToken)
        {
            return await _orchestrator.GetListByOverdueAsync(request, cancellationToken);
        }
    }
}
