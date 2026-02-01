using System.ComponentModel.DataAnnotations;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.WebAPI.Model.Get
{
    public class GetToDosByPriorityDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public ToDoPriority Priority { get; set; }
    }
}
