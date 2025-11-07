using MediatR;
using ToDoList.Application.Common.Exceptions;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Domain.ToDo;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommandHandler : IRequestHandler<ChangeToDoContentCommand, Unit>
    {
        private readonly IToDoRepository _repository;

        public ChangeToDoContentCommandHandler(IToDoRepository repository) =>
            _repository = repository;

        public async Task<Unit> Handle(ChangeToDoContentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(ToDoItem), request.Id);
            }

            entity.EditDate = DateTime.UtcNow;
            entity.Title = request.Title;
            entity.Details = request.Details;

            await _repository.UpdateAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
