using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Application.ToDoItems.ServiceCommands
{
    public class UpdateToDoDeadLinesValidator : AbstractValidator<UpdateToDoDeadLinesCommand>
    {
        public UpdateToDoDeadLinesValidator()
        {
            RuleFor(command => 
                command.Id).NotEqual(Guid.Empty);
            RuleFor(command => 
                command.UserId).NotEqual(Guid.Empty);
        }
    }
}
