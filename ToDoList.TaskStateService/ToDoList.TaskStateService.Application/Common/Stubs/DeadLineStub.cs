using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Application.Common.Stubs
{
    public class DeadLineStub
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public long DeadlineUnix { get; set; }
    }
}
