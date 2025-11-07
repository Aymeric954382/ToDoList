using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Application.ToDoItems.Queries.Containers;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetByPriority
{
    public class GetToDoListByPriorityQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}