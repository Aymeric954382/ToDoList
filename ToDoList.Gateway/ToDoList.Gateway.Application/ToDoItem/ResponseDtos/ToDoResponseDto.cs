using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Application.ToDoItem.ResponseDtos
{
    public class ToDoResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }

    }
}
