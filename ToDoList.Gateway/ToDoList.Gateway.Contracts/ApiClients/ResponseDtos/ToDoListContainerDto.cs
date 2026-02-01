using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.ResponseDtos
{
    public class ToDoListContainerDto
    {
        public List<ToDoItemDto> Items { get; set; }
    }
}
