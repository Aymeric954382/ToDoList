using MediatR;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo
{
    public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, ServiceResult<CreateToDoResponseDto>>
    {
        private readonly ICreateToDoOrchestrator _orchestrator;
        public CreateToDoCommandHandler(ICreateToDoOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }
        public async Task<ServiceResult<CreateToDoResponseDto>> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
        {
           return await _orchestrator.CreateAsync(request, cancellationToken);
        }
    }
}
