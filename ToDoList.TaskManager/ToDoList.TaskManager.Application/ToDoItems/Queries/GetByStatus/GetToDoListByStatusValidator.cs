using FluentValidation;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.ToDoItems.Queries.GetByStatus
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
