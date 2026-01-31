using FluentValidation;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.GetListToDo
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
