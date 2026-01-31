using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get
{
    public class GetToDoListByOverdueDto
    {
       public Guid UserId { get; set; }
    }
}
