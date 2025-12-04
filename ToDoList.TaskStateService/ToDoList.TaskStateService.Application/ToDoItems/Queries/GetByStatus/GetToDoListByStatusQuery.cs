using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.GetByStatus
{
    public class GetToDoListByStatusQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
        public ToDoStatus? Status { get; set; }
    }
}
