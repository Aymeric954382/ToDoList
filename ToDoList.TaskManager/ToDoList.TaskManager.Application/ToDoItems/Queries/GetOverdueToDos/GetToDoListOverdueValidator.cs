using FluentValidation;

namespace ToDoList.TaskManager.Application.ToDoItems.Queries.GetOverdueToDos
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
