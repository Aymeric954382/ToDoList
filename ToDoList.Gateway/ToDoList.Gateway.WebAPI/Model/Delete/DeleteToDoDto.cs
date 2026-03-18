using System.ComponentModel.DataAnnotations;

namespace ToDoList.Gateway.WebAPI.Model.Delete
{
    public class DeleteToDoDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
