using FluentValidation;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.ToDoItems.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityValidator : AbstractValidator<ChangeToDoPriorityCommand>
    {
        public ChangeToDoPriorityValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.UserId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.Priority).Must(priority =>
                priority == null ||
                Enum.IsDefined(typeof(ToDoPriority), priority));
        }
    }
}
