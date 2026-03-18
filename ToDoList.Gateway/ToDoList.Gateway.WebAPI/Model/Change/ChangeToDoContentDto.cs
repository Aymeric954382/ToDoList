using System.ComponentModel.DataAnnotations;

namespace ToDoList.Gateway.WebAPI.Model.Change
{
    public class ChangeToDoContentDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Details { get; set; }
    }
}
