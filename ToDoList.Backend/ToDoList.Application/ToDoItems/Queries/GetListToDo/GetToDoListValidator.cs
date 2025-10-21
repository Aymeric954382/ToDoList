using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListValidator : AbstractValidator<GetToDoListQuery>
    {
        public GetToDoListValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
        }
    }
}
