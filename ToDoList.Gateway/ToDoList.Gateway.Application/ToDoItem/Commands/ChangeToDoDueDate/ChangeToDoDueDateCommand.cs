using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
