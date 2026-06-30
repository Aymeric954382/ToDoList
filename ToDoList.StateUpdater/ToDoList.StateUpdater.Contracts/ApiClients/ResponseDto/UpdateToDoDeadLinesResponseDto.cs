using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Contracts.ApiClients.ResponseDto
{
    public class UpdateToDoDeadLinesResponseDto
    {
        public List<Guid>? SuccessUpdated { get; set; }
        public List<Guid> UpdateRestrictions { get; set; }
        public List<Guid>? FailUpdate { get; set; }
    }
}
