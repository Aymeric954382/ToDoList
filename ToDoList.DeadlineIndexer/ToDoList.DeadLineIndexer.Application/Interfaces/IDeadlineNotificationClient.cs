using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DeadlineIndexer.Domain;

namespace ToDoList.DeadlineIndexer.Application.Interfaces
{
    public interface IDeadlineNotificationClient
    {
        Task SendDeadlinesAsync(List<DeadlineStub> stubs, 
            CancellationToken cancellationToken);
    }
}

