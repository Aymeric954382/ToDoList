using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get
{
    public class TaskStateServiceGetToDoListByOverdueRequestDto
    {
       public Guid UserId { get; set; }
    }
}
