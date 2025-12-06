using ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.ToDoItems.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
