using MediatR;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler 
        : IRequestHandler<DeleteToDoCommand, ServiceResult<DeleteToDoResponseDto>>
    {
        private readonly IDeleteToDoOrchestrator _orchestrator;
        public DeleteToDoCommandHandler(IDeleteToDoOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }
        public async Task<ServiceResult<DeleteToDoResponseDto>> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
        {
            return await _orchestrator.DeleteAsync(request, cancellationToken);
        }
    }
}
