using MediatR;
using ToDoList.TaskManager.Application.Common.Exceptions;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, Unit>
    {
        public readonly IToDoRepository _repository;
        public DeleteToDoCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<Unit> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(ToDoItem), request.Id);
            }

            await _repository.DeleteAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
