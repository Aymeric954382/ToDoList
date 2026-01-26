using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, Unit>
    {
        private readonly ITaskStateServiceApiClientAdapter _serviceApiClient;
        private readonly ITaskManagerApiClientAdapter _managerApiClient;
        public DeleteToDoCommandHandler(ITaskStateServiceApiClientAdapter serviceApiClient, ITaskManagerApiClientAdapter managerApiClient)
        {
            _serviceApiClient = serviceApiClient;
            _managerApiClient = managerApiClient;
        }
        public async Task<Unit> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
        {
            await _managerApiClient.DeleteAsync(request);
            await _serviceApiClient.DeleteAsync(request);

            return Unit.Value;
        }
    }
}
