using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.Application.ToDoItems.Queries.GetByStatus
{
    public class GetToDoListByStatusValidator : AbstractValidator<GetToDoListByStatusQuery>
    {
        public GetToDoListByStatusValidator()
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
