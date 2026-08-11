using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Contracts.Common.Operations
{
    public class RabbitOperation
    {
        public static RabbitOperation Create = new("taskstateservice-create");
        public static RabbitOperation Delete = new("taskstateservice-");
        public static RabbitOperation ChangePriorirty = new("taskstateservice-");
        public static RabbitOperation ChangeStatus = new("taskstateservice-");
        public static RabbitOperation ChangeDueDate = new("taskstateservice-");
        public string Value { get; }

        private RabbitOperation(string value)
        {
            Value = value;
        }
    }
}
