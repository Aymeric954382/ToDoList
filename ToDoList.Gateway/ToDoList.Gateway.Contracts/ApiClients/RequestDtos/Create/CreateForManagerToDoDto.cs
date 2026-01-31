using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Create
{
    public class CreateForManagerToDoDto
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
