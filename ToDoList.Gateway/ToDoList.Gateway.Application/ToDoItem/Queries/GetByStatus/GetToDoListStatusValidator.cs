using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.GetByStatus
{
    public class GetToDoListStatusValidator : AbstractValidator<GetToDoListByStatusQuery>
    {
        public GetToDoListStatusValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
            RuleFor(query =>
                query.Status).NotEmpty().NotNull();
        }
    }
}
