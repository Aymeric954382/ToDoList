using System.ComponentModel.DataAnnotations;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.WebAPI.Models
{
    public class ChangeToDoPriorityDto
    {
        [Required]
        public Guid Id { get; set; }
        public ToDoPriority Priority { get; set; }
    }
}
