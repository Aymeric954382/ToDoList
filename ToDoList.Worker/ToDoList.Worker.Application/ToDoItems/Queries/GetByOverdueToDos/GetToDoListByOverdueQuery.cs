using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Worker.Application.ToDoItems.Queries.Contatiners;

namespace ToDoList.Worker.Application.ToDoItems.Queries.GetByOverdueToDos
{
    public class GetToDoListOverdueQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}
