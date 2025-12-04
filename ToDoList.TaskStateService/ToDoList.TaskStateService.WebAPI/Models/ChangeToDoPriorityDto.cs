using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models
{
    public class ChangeToDoPriorityDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public ToDoPriority Priority { get; set; }
    }
}
