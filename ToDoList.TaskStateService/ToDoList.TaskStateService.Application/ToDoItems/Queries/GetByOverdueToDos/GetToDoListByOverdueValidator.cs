using FluentValidation;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.GetByOverdueToDos
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
