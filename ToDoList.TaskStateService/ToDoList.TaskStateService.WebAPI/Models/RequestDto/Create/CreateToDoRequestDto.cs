using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models.RequestDto.Create
{
    public class CreateToDoRequestDto
    {
        [Required]
        public Guid Id { get; set; }
        public ToDoPriority? Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
