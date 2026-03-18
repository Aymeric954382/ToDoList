using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries
{
    public class TaskManagerGetToDoListByIdsResponseDto
    {
        public IEnumerable<TaskManagerItemResponseDto>? Items { get; init; }
    }
}
