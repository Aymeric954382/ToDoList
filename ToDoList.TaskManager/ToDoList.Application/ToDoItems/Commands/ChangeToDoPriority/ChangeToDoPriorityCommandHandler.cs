using MediatR;
using ToDoList.Application.Common.Exceptions;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Domain.ToDo;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoPriority
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

            entity.EditDate = DateTime.UtcNow;
            entity.Priority = request.Priority;

            await _repository.UpdateAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
