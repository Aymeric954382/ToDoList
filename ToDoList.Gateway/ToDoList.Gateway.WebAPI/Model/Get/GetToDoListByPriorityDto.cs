using System.ComponentModel.DataAnnotations;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.ValueObjects;

namespace ToDoList.Gateway.WebAPI.Model.Get
{
    public class GetToDoListByPriorityDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public ToDoPriority Priority { get; set; }
    }
}
