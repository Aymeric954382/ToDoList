using System.ComponentModel.DataAnnotations;

namespace ToDoList.TaskStateService.WebAPI.Models.RequestDto.Delete
{
    public class DeleteToDoRequestDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
