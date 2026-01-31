using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;
namespace ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change
{
    public class ChangeToDoStatusDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
