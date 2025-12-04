using FluentValidation;

namespace ToDoList.TaskStateService.Application.ToDoItems.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateValidator : AbstractValidator<ChangeToDoDueDateCommand>
    {
        public ChangeToDoDueDateValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.UserId).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.DueDate).Must(date => date > DateTime.Now || date == null);
        }
    }
}
