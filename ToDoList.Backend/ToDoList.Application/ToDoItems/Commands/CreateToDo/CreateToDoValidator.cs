using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.ToDoItems.Commands.CreateToDoItem;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Commands.CreateToDo
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
