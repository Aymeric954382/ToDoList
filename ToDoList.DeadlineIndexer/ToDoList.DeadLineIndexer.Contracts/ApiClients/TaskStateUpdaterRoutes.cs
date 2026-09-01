using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Contracts.ApiClients
{
    public class TaskStateUpdaterRoutes
    {
        public required string BaseAddress { get; init; }  

        public required string UpdateAll { get; init; }
    }
}
