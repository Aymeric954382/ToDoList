using System.ComponentModel.DataAnnotations;

namespace ToDoList.Gateway.WebAPI.Model.Get
{
    public class GetToDoListOverdueDto
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
