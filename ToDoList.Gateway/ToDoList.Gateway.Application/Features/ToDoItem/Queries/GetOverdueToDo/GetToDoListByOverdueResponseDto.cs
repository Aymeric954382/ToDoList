using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.DomainResponseDtos;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo
{
    public class GetToDoListByOverdueResponseDto
    {
        public IEnumerable<ToDoItemDto> Items { get; set; }
    }
}
