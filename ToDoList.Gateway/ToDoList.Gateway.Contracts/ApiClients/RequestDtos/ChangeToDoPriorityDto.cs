using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Contracts.ApiClients.RequestDtos
{
    public class ChangeToDoPriorityDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
