using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Contracts.Common.Interfaces
{
    public interface IRabbitEndpoints
    {
        IReadOnlyCollection<RabbitEndpoint> GetEndpoints();
    }
}