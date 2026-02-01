using System.ComponentModel.DataAnnotations;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.WebAPI.Model.Change
{
    public class ChangeToDoStatusDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
