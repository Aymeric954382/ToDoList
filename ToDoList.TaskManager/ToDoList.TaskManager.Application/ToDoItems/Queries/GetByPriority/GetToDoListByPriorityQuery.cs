using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;
using ToDoList.TaskManager.Application.ToDoItems.Queries.Containers;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.ToDoItems.Queries.GetByPriority
{
    public class GetToDoListByPriorityQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}