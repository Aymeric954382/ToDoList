using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusValidator : AbstractValidator<ChangeToDoStatusCommand>
    {
        public ChangeToDoStatusValidator()
        {
            RuleFor(i =>
                i.Id).NotEqual(Guid.Empty);
            RuleFor(i =>
                i.UserId).NotEqual(Guid.Empty);
        }
    }
}
