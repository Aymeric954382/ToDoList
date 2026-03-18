using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo;

namespace ToDoList.Gateway.Application.Interfaces.Orchestartors
{
    public interface IGetToDoListOrchestrator
    {
        Task<ServiceResult<GetToDoListResponseDto>> GetListAsync(GetToDoListQuery query, CancellationToken cancellationToken);
    }
}
