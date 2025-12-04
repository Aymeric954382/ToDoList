using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Application.ToDoItems.Queries.Containers;

namespace ToDoList.Application.ToDoItems.Queries.GetOverdueToDos
{
    public class GetToDoListOverdueQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}
