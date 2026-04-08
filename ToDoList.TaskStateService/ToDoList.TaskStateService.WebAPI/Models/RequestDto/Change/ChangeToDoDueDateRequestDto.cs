using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models.RequestDto.Change
{
    public class ChangeToDoDueDateRequestDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }
    }
}
