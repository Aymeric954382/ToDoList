using FluentValidation;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoContent
{
    public class ChangeToDoContentValidator : AbstractValidator<ChangeToDoContentCommand>
    {
        public ChangeToDoContentValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.UserId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.Title).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(command =>
                command.Details).MaximumLength(200);
        }
    }
}
