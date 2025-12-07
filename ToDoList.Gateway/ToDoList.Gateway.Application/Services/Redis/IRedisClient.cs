using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Stubs;

namespace ToDoList.Gateway.Application.Services.Redis
{
    public interface IRedisClient
    {
        Task AddDeadlineStubAsync(DeadLineStub stub);
    }
}
