using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommandHandler : IRequestHandler<ChangeToDoContentCommand, Unit>
    {
        private readonly ITaskManagerApiClientAdapter _clientAdapter;
        public ChangeToDoContentCommandHandler(ITaskManagerApiClientAdapter clientAdapter)
        {
            _clientAdapter = clientAdapter;
        }
        public async Task<Unit> Handle(ChangeToDoContentCommand request, CancellationToken cancellationToken)
        {
            await _clientAdapter.ChangeContentAsync(request);

            return Unit.Value;
        }
    }
}
