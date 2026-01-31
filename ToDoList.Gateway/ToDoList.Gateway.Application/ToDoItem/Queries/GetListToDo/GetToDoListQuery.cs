using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.GetListToDo
{
    public class GetToDoListQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
        
    }
}
