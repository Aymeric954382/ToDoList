using FluentValidation;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetByPriority
{
    public class GetToDoListByPriorityValidator : AbstractValidator<GetToDoListByPriorityQuery>
    {
        public GetToDoListByPriorityValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
            RuleFor(query =>
                query.Priority).Must(priority =>
                priority == null ||
                Enum.IsDefined(typeof(ToDoPriority), priority));
        }
    }
}
