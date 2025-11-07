using FluentValidation;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetByStatus
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
