using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Contracts.Common.Interfaces;
using ToDoList.TaskStateService.Contracts.Common.Operations;

namespace ToDoList.TaskStateService.Contracts.Common
{
    public sealed class RabbitEndpoints : IRabbitEndpoints
    {
        public static readonly RabbitEndpoint Create =
            new("command", "taskstatesservice.commands", RabbitOperation.Create.Value);
        public static readonly RabbitEndpoint Delete =
            new("command", "taskstatesservice.commands", RabbitOperation.Delete.Value);
        public static readonly RabbitEndpoint ChangePriority =
            new("command", "taskstatesservice.commands", RabbitOperation.ChangePriorirty.Value);
        public static readonly RabbitEndpoint ChangeStatus =
            new("command", "taskstatesservice.commands", RabbitOperation.ChangeStatus.Value);
        public static readonly RabbitEndpoint ChangeDueDate =
            new("command", "taskstatesservice.commands", RabbitOperation.ChangeDueDate.Value);

        public IReadOnlyCollection<RabbitEndpoint> GetEndpoints() =>
            [
                Create,
                Delete,
                ChangePriority,
                ChangeStatus,
                ChangeDueDate
            ];


    }
}
