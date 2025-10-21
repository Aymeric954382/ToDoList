using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetByStatus
{
    public class GetToDoByStatusValidator : AbstractValidator<GetToDoByStatusQuery>
    {
        public GetToDoByStatusValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
            RuleFor(query =>
                query.Status).Must(status =>
                status == null ||
                Enum.IsDefined(typeof(ToDoStatus), status));
        }

    }
}
