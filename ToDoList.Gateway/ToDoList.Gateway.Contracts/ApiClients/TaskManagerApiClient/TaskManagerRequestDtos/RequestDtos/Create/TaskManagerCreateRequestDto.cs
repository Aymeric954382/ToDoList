using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create
{
    public class TaskManagerCreateRequestDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
