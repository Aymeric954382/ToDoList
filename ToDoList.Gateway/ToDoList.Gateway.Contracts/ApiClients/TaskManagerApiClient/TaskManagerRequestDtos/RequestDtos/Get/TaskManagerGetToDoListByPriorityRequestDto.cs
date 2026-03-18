using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.ValueObjects;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get
{
    public class TaskManagerGetToDoListByPriorityRequestDto
    {
        public Guid UserId { get; set; }
        public ToDoPriority Priority { get; set; }
    }
}
