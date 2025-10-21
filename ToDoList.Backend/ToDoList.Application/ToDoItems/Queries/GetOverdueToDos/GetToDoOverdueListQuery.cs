using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Application.ToDoItems.Queries.Containers;
using ToDoList.Application.ToDoItems.Queries.ResponseDtos;

namespace ToDoList.Application.ToDoItems.Queries.GetOverdueToDos
{
    public class GetToDoOverdueListQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}
