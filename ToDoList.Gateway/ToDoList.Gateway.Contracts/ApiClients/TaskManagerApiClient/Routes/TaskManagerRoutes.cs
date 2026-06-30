using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes
{
    public class TaskManagerRoutes
    {
        public required string ChangeContent { get; set; }
        public required string Delete { get; set; }
        public required string Create { get; set; }
        public required string GetToDoList { get; set; }
        public required string GetToDoByIdList { get; set; }
    }
}
