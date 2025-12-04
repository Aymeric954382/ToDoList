using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.TaskStateService.Application.ToDoItems.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
