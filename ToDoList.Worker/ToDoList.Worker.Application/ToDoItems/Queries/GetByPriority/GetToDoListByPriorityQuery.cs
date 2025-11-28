using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Worker.Application.ToDoItems.Queries.Contatiners;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.Application.ToDoItems.Queries.GetByPriority
{
    public class GetToDoListByPriorityQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
