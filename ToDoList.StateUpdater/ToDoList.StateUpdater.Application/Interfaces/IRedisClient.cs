using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.StateUpdater.Application.Stubs;

namespace ToDoList.StateUpdater.Application.Interfaces
{
    public interface IRedisClient
    {
        Task<DeadlineStub[]> PopDueStubsAsync(int batchSize = 100, int leadSeconds = 60);
    }
}
