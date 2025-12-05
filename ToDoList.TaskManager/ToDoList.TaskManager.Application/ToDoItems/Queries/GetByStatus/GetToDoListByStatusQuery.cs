using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;
using ToDoList.TaskManager.Application.ToDoItems.Queries.Containers;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.ToDoItems.Queries.GetByStatus
{
    public class GetToDoListByStatusQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
        public ToDoStatus? Status { get; set; }
    }
}