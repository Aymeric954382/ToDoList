using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Contracts.Common.Operations;

namespace ToDoList.TaskStateService.Contracts.Common.Interfaces
{
    public interface IRabbitOperation
    {
        static abstract RabbitOperation Operation { get; }
    }
}
