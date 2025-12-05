using FluentValidation;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoValidator : AbstractValidator<CreateToDoCommand>
    {
        public CreateToDoValidator()
        {
            RuleFor(command =>
                command.UserId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.Title).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(command =>
                command.Details).MaximumLength(200);
            RuleFor(command =>
                command.DueDate).Must(date => date > DateTime.Now || date == null);
            RuleFor(command =>
            command.Priority).Must(priority =>
            priority == null ||
            Enum.IsDefined(typeof(ToDoPriority), priority));
        }
    }
}
