using FluentValidation;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoValidator : AbstractValidator<CreateToDoCommand>
    {
        public CreateToDoValidator()
        {
            RuleFor(command =>
                command.UserId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.DueDate).Must(date => date > DateTime.Now || date == null);
            RuleFor(command =>
            command.Priority).Must(priority =>
            priority == null ||
            Enum.IsDefined(typeof(ToDoPriority), priority));
        }
    }
}
