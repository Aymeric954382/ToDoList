using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByStatus;

namespace ToDoList.Gateway.Application.Interfaces.Orchestartors
{
    public interface IGetToDoListByStatusOrchestrator
    {
        Task<ServiceResult<GetToDoListByStatusResponseDto>> GetListByStatusAsync(GetToDoListByStatusQuery query, CancellationToken cancellationToken);
    }
}
