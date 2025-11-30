using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Worker.Application.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoValidator : AbstractValidator<DeleteToDoCommand>
    {
        public DeleteToDoValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
            RuleFor(command =>
                command.UserId).NotEqual(Guid.Empty);
        }
    }
}
