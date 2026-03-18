using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.ValueObjects;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get
{
    public class TaskStateServiceGetToDoListByStatusRequestDto
    {
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
