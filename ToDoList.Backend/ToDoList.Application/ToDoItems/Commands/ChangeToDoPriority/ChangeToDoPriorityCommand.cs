using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
