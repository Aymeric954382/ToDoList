using System.ComponentModel.DataAnnotations;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.WebAPI.Models
{
    public class CreateToDoDto
    {
        [Required]
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
