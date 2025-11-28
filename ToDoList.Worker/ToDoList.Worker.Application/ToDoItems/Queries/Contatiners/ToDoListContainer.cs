using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.ToDoItems.Queries.ResponseDtos;

namespace ToDoList.Worker.Application.ToDoItems.Queries.Contatiners
{
    public class ToDoListContainer
    {
        public IList<ToDoResponseDto> ToDoItems { get; set; }
    }
}
