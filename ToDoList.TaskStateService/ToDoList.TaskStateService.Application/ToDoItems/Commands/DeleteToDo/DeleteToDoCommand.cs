using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.TaskStateService.Application.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
