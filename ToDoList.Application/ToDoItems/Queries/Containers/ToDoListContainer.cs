using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.MappingMark;
using ToDoList.Application.ToDoItems.Queries.Dtos;
using ToDoList.Domain.ToDo;

namespace ToDoList.Application.ToDoItems.Queries.Containers
{
    public class ToDoListContainer
    {
        public IList<ToDoDto> ToDoItems { get; set; }
    }
}
