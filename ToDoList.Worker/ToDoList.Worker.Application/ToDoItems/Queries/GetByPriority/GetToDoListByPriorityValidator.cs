using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.Application.ToDoItems.Queries.GetByPriority
{
    public class GetToDoListByPriorityValidator : AbstractValidator<GetToDoListByPriorityQuery>
    {
        public GetToDoListByPriorityValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
            RuleFor(query =>
                query.Priority).Must(priority =>
                priority == null ||
                Enum.IsDefined(typeof(ToDoPriority), priority));
        }
    }
}
