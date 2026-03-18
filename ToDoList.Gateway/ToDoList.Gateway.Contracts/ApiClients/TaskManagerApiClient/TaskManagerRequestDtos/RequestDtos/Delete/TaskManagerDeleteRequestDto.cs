using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete
{
    public class TaskManagerDeleteRequestDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
