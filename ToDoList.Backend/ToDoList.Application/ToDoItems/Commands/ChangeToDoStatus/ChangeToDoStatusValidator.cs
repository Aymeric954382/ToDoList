using FluentValidation;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusValidator : AbstractValidator<ChangeToDoStatusCommand>
    {
        public ChangeToDoStatusValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.UserId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.Status).Must(status => Enum.IsDefined(typeof(ToDoStatus), status));
        }
    }
}
