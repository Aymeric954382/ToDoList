using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.RequestDtos
{
    public class CreateForServiceToDoDto
    {
        public Guid Id { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
