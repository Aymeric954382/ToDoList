using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommandHandler : IRequestHandler<ChangeToDoPriorityCommand, Unit>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        public ChangeToDoPriorityCommandHandler(ITaskStateServiceApiClientAdapter clientAdapter)
        {
            _clientAdapter = clientAdapter;
        }
        public async Task<Unit> Handle(ChangeToDoPriorityCommand request, CancellationToken cancellationToken)
        {
            await _clientAdapter.ChangePriorityAsync(request);

            return Unit.Value;
        }
    }
}
