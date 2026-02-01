using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo
{
    public class CreateToDoValidator : AbstractValidator<CreateToDoCommand>
    {
        public CreateToDoValidator()
        {
            RuleFor(i =>
                i.UserId).NotEqual(Guid.Empty);
        }
    }
}
