using System.ComponentModel.DataAnnotations;

namespace ToDoList.Gateway.WebAPI.Model.Get
{
    public class GetAllToDosDto
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
