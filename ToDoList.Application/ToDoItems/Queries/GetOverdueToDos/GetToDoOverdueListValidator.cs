using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.ToDoItems.Queries.GetOverdueToDos
{
    public class GetToDoOverdueListValidator : AbstractValidator<GetToDoOverdueListQuery>
    {
        public GetToDoOverdueListValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
        }

    }
}
