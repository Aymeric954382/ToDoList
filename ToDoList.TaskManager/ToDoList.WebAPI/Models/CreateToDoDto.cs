using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.WebAPI.Models
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
