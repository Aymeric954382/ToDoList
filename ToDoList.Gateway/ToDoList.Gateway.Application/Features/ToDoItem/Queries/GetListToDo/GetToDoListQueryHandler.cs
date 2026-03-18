using MediatR;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo
{
    public class GetToDoListQueryHandler 
        : IRequestHandler<GetToDoListQuery, 
            ServiceResult<GetToDoListResponseDto>>
    {
        private readonly IGetToDoListOrchestrator _orchestrator;
        public GetToDoListQueryHandler(IGetToDoListOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }
        public async Task<ServiceResult<GetToDoListResponseDto>> Handle(
            GetToDoListQuery request, 
            CancellationToken cancellationToken)
        {
            return await _orchestrator.GetListAsync(request, cancellationToken);
        }
    }
}
