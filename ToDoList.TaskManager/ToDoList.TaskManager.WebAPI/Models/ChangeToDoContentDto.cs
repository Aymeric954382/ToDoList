using System.ComponentModel.DataAnnotations;

namespace ToDoList.TaskManager.WebAPI.Models
{
    public class ChangeToDoContentDto
    {
        [Required]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
