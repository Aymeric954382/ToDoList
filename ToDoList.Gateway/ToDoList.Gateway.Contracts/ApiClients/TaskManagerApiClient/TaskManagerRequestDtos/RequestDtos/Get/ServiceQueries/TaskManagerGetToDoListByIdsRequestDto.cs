using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get.ServiceQueries
{
    public class TaskManagerGetToDoListByIdsRequestDto
    {
        public IEnumerable<Guid>? Ids { get; init; }
        public Guid UserId { get; init;}
    }
}
