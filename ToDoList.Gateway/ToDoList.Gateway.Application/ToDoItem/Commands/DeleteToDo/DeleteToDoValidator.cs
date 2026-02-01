using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoValidator : AbstractValidator<DeleteToDoCommand>
    {
        public DeleteToDoValidator()
        {
            RuleFor(i => 
                i.Id).NotEqual(Guid.Empty);
            RuleFor(i =>
                i.UserId).NotEqual(Guid.Empty);
        }
    }
}
