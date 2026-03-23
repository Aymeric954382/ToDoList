using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListResponseDto
    {
        public IEnumerable<ToDoItemDto>? Items { get; set; }
    }
}
