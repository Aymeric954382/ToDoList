using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoPriority;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommandHandler
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        public ChangeToDoStatusCommandHandler(ITaskStateServiceApiClientAdapter clientAdapter)
        {
            _clientAdapter = clientAdapter;
        }
        public async Task<Unit> Handle(ChangeToDoStatusCommand request, CancellationToken cancellationToken)
        {
            await _clientAdapter.ChangeStatusAsync(request);

            return Unit.Value;
        }
    }
}
