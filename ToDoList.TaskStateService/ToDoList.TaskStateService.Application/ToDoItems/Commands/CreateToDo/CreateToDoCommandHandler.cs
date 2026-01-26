using MediatR;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, Guid>
    {
        private readonly IToDoRepository _repository;
        public CreateToDoCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<Guid> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
        {
            var toDoItem = new ToDoItem
            {
                UserId = request.UserId,
                Id = request.Id,
                Status = ToDoStatus.Active,
                DueDate = request.DueDate,
                CreationDate = DateTime.UtcNow,
                EditDate = null,
                Priority = request.Priority
            };

            await _repository.AddAsync(toDoItem, cancellationToken);

            return toDoItem.Id;
        }
    }
}
