using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Commands.CreateToDoItem
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
