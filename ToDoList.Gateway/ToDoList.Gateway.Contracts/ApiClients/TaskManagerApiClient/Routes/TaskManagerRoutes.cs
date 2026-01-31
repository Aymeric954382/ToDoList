using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes
{
    public class TaskManagerRoutes
    {
        public string ChangeContent { get; set; }
        public string Delete { get; set; }
        public string Create { get; set; }
        public string GetToDoList { get; set; }
    }
}
