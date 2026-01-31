using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommandHandler : IRequestHandler<ChangeToDoDueDateCommand, Unit>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientAdapter;
        public ChangeToDoDueDateCommandHandler(ITaskStateServiceApiClientAdapter clientAdapter)
        {
            _clientAdapter = clientAdapter;
        }

        public async Task<Unit> Handle(ChangeToDoDueDateCommand request, CancellationToken cancellationToken)
        {
            await _clientAdapter.ChangeDueDateAsync(request);

            return Unit.Value;
        }
    }
}
