using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListResponseDto
    {
        public IEnumerable<ToDoItemDto> Items { get; set; }
    }
}
