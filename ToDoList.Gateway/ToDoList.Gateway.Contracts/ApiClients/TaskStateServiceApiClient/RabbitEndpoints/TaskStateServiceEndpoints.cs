using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;
using ToDoList.Gateway.Contracts.Interfaces;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.RabbitEndpoints
{
    public sealed class TaskStateServiceEndpoints : IRabbitEndpoints
    {
        public static readonly RabbitEndpoint Create =
            new ("commands", "taskstateservice.commands", "taskstateservice.create");

        public static readonly RabbitEndpoint Delete =
            new("commands", "taskstateservice.commands", "taskstateservice.delete");

        public static readonly RabbitEndpoint ChangePriority =
            new("commands", "taskstateservice.commands", "taskstateservice.change-priority");

        public static readonly RabbitEndpoint ChangeDueDate =
            new("commands", "taskstateservice.commands", "taskstateservice.change-duedate");

        public static readonly RabbitEndpoint ChangeStatus =
            new("commands", "taskstateservice.commands", "taskstateservice.change-status");

        public IReadOnlyCollection<RabbitEndpoint> GetEndpoints() =>
            [
                Create,
                Delete,
                ChangeDueDate,
                ChangePriority,
                ChangeStatus
            ];
    }
}
