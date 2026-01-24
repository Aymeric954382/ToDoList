using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes
{
    public class TaskStateServiceRoutes
    {
        public string ChangeDueDate { get; set; }
        public string ChangePriority { get; set; }
        public string ChangeStatus { get; set; }
        public string Delete { get; set; }
        public string Create { get; set; }
    }
}
