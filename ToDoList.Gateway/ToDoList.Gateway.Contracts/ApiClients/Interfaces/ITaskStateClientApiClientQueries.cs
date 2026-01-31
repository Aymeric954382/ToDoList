using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskStateClientApiClientQueries
    {
        Task<ToDoListContainerDto> GetToDoListAsync(GetToDoListDto dto);
        Task<ToDoListContainerDto> GetToDoListByPriorityAsync(GetToDoListByPriorityDto dto);
        Task<ToDoListContainerDto> GetToDoListByStatusAsync(GetToDoListByStatusDto dto);
        Task<ToDoListContainerDto> GetToDoListByOverdueAsync(GetToDoListByOverdueDto dto);
    }
}
