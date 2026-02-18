using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.GetOverdueToDo
{
    public class GetToDoListOverdueValidator : AbstractValidator<GetToDoListByOverdueQuery>
    {
        public GetToDoListOverdueValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
        }
    }
}
