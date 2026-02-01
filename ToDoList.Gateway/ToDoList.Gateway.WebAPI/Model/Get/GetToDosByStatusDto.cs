using System.ComponentModel.DataAnnotations;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.WebAPI.Model.Get
{
    public class GetToDosByStatusDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public ToDoStatus Status { get; set; }
    }
}
