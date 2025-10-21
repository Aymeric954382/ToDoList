using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.WebAPI.Models
{
    public class ChangeToDoStatusDto
    {
        public Guid Id { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
