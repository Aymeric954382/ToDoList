using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.WebAPI.Models
{
    public class ChangeToDoPriorityDto
    {
        [Required]
        public Guid Id { get; set; }
        public ToDoPriority Priority { get; set; }
    }
}
