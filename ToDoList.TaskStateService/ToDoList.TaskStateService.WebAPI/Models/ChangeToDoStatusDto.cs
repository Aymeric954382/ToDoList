using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models
{
    public class ChangeToDoStatusDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public ToDoStatus Status { get; set; }
    }
}
