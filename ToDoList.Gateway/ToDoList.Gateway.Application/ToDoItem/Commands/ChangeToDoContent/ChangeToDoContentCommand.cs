using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
