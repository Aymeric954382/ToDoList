using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Create
{
    public class TaskStateServiceCreateRequestDto
    {
        public Guid Id { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
