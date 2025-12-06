using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
