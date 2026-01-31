using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.ToDoItems.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommandHandler : IRequestHandler<ChangeToDoPriorityCommand, Unit>
    {
        public readonly IToDoRepository _repository;
        public ChangeToDoPriorityCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<Unit> Handle(ChangeToDoPriorityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(ToDoItem), request.Id);
            }
            if (entity.Priority == request.Priority)
            {
                throw new IdenticalReplacementException(nameof(ToDoItem), entity.Priority, entity.Id);
            }

            entity.EditDate = DateTime.UtcNow;
            entity.Priority = request.Priority;

            await _repository.UpdateAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}