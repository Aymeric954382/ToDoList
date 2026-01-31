using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskManagerApiClientQueries
    {
        Task<ToDoListContainerDto> GetToDoListAsync(GetToDoListDto dto);
    }
}
