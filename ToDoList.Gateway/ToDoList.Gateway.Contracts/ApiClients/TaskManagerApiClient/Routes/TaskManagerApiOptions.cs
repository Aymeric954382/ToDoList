using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes
{
    public class TaskManagerApiOptions
    {
        public required string BaseUrl { get; set; }
        public required TaskManagerRoutes Routes { get; set; }
    }
}
