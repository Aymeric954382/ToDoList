using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds
{
    public class GetToDoListByIdsRequestQuery : IQuery<ServiceResult<GetToDoListByIdsResponseDto>>
    {
        public IEnumerable<Guid>? Ids { get; set; }
        public Guid UserId { get; set; }
    }
}
