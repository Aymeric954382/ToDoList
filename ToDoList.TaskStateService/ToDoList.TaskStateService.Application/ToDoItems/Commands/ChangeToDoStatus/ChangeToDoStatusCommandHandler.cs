using MediatR;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.ToDoItems.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommandHandler : IRequestHandler<ChangeToDoStatusCommand, Unit>
    {
        public readonly IToDoRepository _repository;

        public ChangeToDoStatusCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<Unit> Handle(ChangeToDoStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(ToDoItem), request.Id);
            }

            entity.Status = request.Status;
            entity.EditDate = DateTime.UtcNow;

            await _repository.UpdateAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
