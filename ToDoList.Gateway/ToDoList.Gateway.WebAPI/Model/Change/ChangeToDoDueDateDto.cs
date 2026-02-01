using System.ComponentModel.DataAnnotations;

namespace ToDoList.Gateway.WebAPI.Model.Change
{
    public class ChangeToDoDueDateDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
