using MediatR;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByStatus
{
    public class GetToDoListByStatusQueryHandler 
        : IRequestHandler<GetToDoListByStatusQuery, 
            ServiceResult<GetToDoListByStatusResponseDto>>
    {
        private readonly IGetToDoListByStatusOrchestrator _orchestrator;
        public GetToDoListByStatusQueryHandler(IGetToDoListByStatusOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }
        public async Task<ServiceResult<GetToDoListByStatusResponseDto>> Handle(
            GetToDoListByStatusQuery request, 
            CancellationToken cancellationToken)
        {
            return await _orchestrator.GetListByStatusAsync(request, cancellationToken);
        }
    }
}
