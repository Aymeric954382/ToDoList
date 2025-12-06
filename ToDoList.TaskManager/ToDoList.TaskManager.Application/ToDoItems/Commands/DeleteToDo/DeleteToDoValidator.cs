using FluentValidation;

namespace ToDoList.TaskManager.Application.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoValidator : AbstractValidator<DeleteToDoCommand>
    {
        public DeleteToDoValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.UserId).NotEqual(Guid.Empty);
        }
    }
}
