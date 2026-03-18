using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo
{
    public class GetToDoListByOverdueValidator : AbstractValidator<GetToDoListByOverdueQuery>
    {
        public GetToDoListByOverdueValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
        }
    }
}
