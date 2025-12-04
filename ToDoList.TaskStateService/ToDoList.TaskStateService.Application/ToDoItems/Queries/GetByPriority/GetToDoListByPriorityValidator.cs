using FluentValidation;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.GetByPriority
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
