using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo
{
    public class CreateToDoCommand : IWithResultCommand<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
