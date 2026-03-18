using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.ServiceQueries
{
    public class TaskStateServiceGetToDoListByIdsResponseDto
    {
        public IEnumerable<TaskStateServiceItemResponseDto>? Items { get; init; }
    }
}
