using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get
{
    public class TaskManagerGetToDoListRequestDto
    {
        Guid UserId { get; set; }
    }
}
