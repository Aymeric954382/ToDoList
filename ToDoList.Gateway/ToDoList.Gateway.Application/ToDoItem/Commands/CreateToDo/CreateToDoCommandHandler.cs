using MediatR;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo
{
    public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, Guid>
    {
        private readonly ITaskStateServiceApiClientAdapter _serviceApiClient;
        private readonly ITaskManagerApiClientAdapter _managerApiClient;
        public CreateToDoCommandHandler(ITaskStateServiceApiClientAdapter serviceApiClient, ITaskManagerApiClientAdapter managerApiClient)
        {
            _serviceApiClient = serviceApiClient;
            _managerApiClient = managerApiClient;
        }
        public async Task<Guid> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
        {
            var result = await _managerApiClient.CreateAsync(request);

            await _serviceApiClient.CreateAsync(request, result);

            return result;
        }
    }
}
