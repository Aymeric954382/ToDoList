using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get
{
    public class GetToDoListOverdueDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
