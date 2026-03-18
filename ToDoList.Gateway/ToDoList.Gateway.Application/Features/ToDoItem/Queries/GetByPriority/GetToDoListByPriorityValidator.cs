using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority
{
    public class GetToDoListByPriorityValidator : AbstractValidator<GetToDoListByPriorityQuery>
    {
        public GetToDoListByPriorityValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
            RuleFor(query =>
                query.Priority).NotNull().NotEmpty();
        }
    }
}
