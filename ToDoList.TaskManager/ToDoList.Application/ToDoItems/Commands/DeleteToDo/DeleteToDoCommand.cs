using ToDoList.Application.Interfaces.Command_QuerySpliter;

namespace ToDoList.Application.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
