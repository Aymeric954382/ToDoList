using MediatR;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Commands.CreateToDoItem;
using ToDoList.Domain.ToDo;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Commands.CreateToDo
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
                Id = Guid.NewGuid(),
                Title = request.Title,
                Details = request.Details,
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
