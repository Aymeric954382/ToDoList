using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Contracts.ApiClients
{
    public class TaskStateServiceRoutes
    {
        public required string BaseAddress { get; init; }

        public required string UpdateAll { get; init; }
    }
}
