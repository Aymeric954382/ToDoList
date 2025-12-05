using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;
using ToDoList.TaskManager.Application.ToDoItems.Queries.Containers;

namespace ToDoList.TaskManager.Application.ToDoItems.Queries.GetOverdueToDos
{
    public class GetToDoListOverdueQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}
