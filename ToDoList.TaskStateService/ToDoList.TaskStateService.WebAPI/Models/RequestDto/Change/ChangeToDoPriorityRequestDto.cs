using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models.RequestDto.Change
{
    public class ChangeToDoPriorityRequestDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public ToDoPriority Priority { get; set; }
    }
}
