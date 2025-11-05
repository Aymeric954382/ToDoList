using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoPriority
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
