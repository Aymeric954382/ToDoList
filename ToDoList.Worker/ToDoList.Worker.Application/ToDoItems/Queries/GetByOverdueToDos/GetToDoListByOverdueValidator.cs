using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Worker.Application.ToDoItems.Queries.GetByOverdueToDos
{
    public class GetToDoListOverdueValidator : AbstractValidator<GetToDoListOverdueQuery>
    {
        public GetToDoListOverdueValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
        }

    }
}
