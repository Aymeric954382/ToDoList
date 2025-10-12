using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoContent
{
    public class ChangeToDoContentValidator : AbstractValidator<ChangeToDoContentCommand>
    {
        ChangeToDoContentValidator()
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
