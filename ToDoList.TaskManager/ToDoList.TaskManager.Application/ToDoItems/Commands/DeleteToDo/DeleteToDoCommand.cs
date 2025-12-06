using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;

namespace ToDoList.TaskManager.Application.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
