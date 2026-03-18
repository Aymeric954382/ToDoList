using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get.ServiceQueries
{
    public class TaskStateServiceGetToDoListByIdsRequestDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init;}
    }
}
