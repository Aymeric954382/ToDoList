using System.ComponentModel.DataAnnotations;

namespace ToDoList.TaskManager.WebAPI.Models.Delete
{
    public class DeleteToDoDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
