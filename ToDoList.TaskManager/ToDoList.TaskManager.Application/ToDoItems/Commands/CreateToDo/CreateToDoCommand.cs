using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoCommand : IWithResultCommand<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
