using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;
using ToDoList.Gateway.Contracts.Interfaces;

namespace ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.RabbitEndpoints
{
    public sealed class TaskManagerEndpoints : IRabbitEndpoints
    {
        public static readonly RabbitEndpoint Create =
            new("commands", "taskmanager.commands", "taskmanager.create");

        public static readonly RabbitEndpoint Delete =
            new("commands", "taskmanager.commands", "taskmanager.delete");

        public static readonly RabbitEndpoint Change =
            new("commands", "taskmanager.commands", "taskmanager.change-title-description");

        public IReadOnlyCollection<RabbitEndpoint> GetEndpoints() =>
            [
                Create,
                Delete,
                Change
            ];

    }
}


