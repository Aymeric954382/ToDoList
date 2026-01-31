using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get
{
    public class GetToDoListByStatusDto
    {
        public ToDoStatus Status { get; set; }
    }
}
