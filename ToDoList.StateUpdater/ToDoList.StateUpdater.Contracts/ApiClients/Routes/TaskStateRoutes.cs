using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Contracts.ApiClients.Routes
{
    public sealed class TaskStateRoutes
    {
        public required string UpdateDeadLines { get; init; } = "tasks/update-deadlines";
    }
}
