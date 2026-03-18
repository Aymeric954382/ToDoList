using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority;

namespace ToDoList.Gateway.Application.Interfaces.Orchestartors
{
    public interface IGetToDoListByPriorityOrchestrator
    {
        Task<ServiceResult<GetToDoListByPriorityResponseDto>> GetListByPriorityAsync(GetToDoListByPriorityQuery query, CancellationToken cancellationToken);
    }
}
