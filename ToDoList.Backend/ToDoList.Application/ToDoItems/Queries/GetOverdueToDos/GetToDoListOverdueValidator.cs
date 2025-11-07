using FluentValidation;

namespace ToDoList.Application.ToDoItems.Queries.GetOverdueToDos
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
