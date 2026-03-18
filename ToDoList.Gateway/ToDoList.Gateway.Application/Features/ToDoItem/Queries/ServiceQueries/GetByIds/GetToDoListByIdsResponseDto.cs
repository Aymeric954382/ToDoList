using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds
{
    public class GetToDoListByIdsResponseDto
    {
        public IEnumerable<Guid> Ids { get; set; }
        public Guid UserId { get; set; }
    }
}
