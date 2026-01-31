using MediatR;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.ToDoItems.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommandHandler : IRequestHandler<ChangeToDoDueDateCommand, Unit>
    {
        public readonly IToDoRepository _repository;
        public ChangeToDoDueDateCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<Unit> Handle(ChangeToDoDueDateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId == request.Id)
            {
                throw new NotFoundException(nameof(ToDoItems), request.Id);
            }
            if (request.DueDate == entity.DueDate)
            {
                throw new IdenticalReplacementException(nameof(ToDoItem), request.DueDate, request.Id);
            }

            entity.EditDate = DateTime.UtcNow;
            entity.DueDate = request.DueDate;

            await _repository.UpdateAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
