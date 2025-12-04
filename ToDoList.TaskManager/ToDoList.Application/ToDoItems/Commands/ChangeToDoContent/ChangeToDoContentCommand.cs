using ToDoList.Application.Interfaces.Command_QuerySpliter;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
