using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Application.ToDoItems.Queries.Containers;

namespace ToDoList.Application.ToDoItems.Queries.GetPriorityQueries.GetToDoLowPriority
{
    public class GetToDoLowPriorityQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}
