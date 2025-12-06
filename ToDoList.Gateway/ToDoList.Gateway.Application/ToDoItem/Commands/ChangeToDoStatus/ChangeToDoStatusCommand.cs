using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
