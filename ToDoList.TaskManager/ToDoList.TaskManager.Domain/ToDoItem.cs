using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Domain
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

    }
}
