using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.WebAPI.Models
{
    public class ChangeToDoPriorityDto
    {
        public Guid Id { get; set; }
        public ToDoPriority Priority { get; set; }
    }
}
