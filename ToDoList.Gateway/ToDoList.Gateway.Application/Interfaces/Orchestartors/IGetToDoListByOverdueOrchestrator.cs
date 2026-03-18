using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo;

namespace ToDoList.Gateway.Application.Interfaces.Orchestartors
{
    public interface IGetToDoListByOverdueOrchestrator
    {
        Task<ServiceResult<GetToDoListByOverdueResponseDto>> GetListByOverdueAsync(GetToDoListByOverdueQuery query, CancellationToken cancellationToken);
    }
}
