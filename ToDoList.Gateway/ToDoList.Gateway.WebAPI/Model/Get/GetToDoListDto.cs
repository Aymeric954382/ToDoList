using System.ComponentModel.DataAnnotations;

namespace ToDoList.Gateway.WebAPI.Model.Get
{
    public class GetToDoListDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]                                  
        public Guid UserId { get; set; }  
    }
}
