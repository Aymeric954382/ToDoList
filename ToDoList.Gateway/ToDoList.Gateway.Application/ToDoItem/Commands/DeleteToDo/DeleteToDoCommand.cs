using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
