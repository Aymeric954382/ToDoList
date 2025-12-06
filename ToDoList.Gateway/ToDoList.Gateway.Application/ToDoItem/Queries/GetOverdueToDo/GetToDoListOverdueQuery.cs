using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.GetOverdueToDo
{
    public class GetToDoListOverdueQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}
