using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.Application.ToDoItems.Commands.CreateToDo
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
