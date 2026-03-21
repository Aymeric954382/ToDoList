using FluentValidation;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListValidator : AbstractValidator<GetToDoListQuery>
    {
        public GetToDoListValidator()
        {
            RuleFor(query =>
                query.UserId).NotEqual(Guid.Empty);
        }
    }
}
