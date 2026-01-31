using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes
{
    public class TaskManagerApiOptions
    {
        public string BaseUrl { get; set; }
        public TaskManagerRoutes Routes { get; set; }
    }
}
