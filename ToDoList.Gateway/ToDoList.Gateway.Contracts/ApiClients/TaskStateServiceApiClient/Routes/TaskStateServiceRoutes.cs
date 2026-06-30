using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes
{
    public class TaskStateServiceRoutes
    {
        public required string ChangeDueDate { get; set; }
        public required string ChangePriority { get; set; }
        public required string ChangeStatus { get; set; }
        public required string Delete { get; set; }
        public required string Create { get; set; }
        public required string GetToDoList { get; set; }
        public required string GetToDoListByOverdue { get; set; }
        public required string GetToDoListByStatus { get; set; }
        public required string GetToDoListByPriority { get; set; }
        public required string GetToDoByIdList { get; set; }


    }
}
