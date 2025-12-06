using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Domain
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
