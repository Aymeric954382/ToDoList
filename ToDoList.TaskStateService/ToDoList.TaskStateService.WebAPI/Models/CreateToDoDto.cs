using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models
{
    public class CreateToDoDto
    {
        [Required]
        public DateTime? DueDate { get; set; }
        [Required]
        public ToDoPriority? Priority { get; set; }
    }
}
