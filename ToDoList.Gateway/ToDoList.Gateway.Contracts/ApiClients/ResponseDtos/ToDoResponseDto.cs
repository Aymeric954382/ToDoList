using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Contracts.ApiClients.ResponseDtos
{
    public class ToDoResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }

    }
}
