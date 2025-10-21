using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoDueDate
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
