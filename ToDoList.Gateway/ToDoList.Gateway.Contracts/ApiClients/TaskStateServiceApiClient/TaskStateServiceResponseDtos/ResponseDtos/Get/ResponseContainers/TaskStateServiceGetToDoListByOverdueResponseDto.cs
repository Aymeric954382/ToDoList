using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers
{
    public class TaskStateServiceGetToDoListByOverdueResponseDto
    {
        public IEnumerable<TaskStateServiceItemResponseDto>? Items { get; init; }
    }
}
