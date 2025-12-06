using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Contracts.ApiClients.RequestDtos
{
    public class ChangeToDoDueDateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
