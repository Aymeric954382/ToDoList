using System.ComponentModel.DataAnnotations;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.WebAPI.Model.Get
{
    public class GetToDosByOverdueDto
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
