using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
