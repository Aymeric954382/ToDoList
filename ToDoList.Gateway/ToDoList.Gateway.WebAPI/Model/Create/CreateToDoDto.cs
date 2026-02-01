using System.ComponentModel.DataAnnotations;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.WebAPI.Model.Create
{
    public class CreateToDoDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
