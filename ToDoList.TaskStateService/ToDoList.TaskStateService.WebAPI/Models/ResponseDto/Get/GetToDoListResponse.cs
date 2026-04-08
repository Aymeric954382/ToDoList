using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models.ResponseDto.Get
{
    public class GetToDoListResponse
    {
        public Guid Id { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
