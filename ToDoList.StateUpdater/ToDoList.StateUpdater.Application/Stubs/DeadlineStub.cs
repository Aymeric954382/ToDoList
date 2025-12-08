using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Application.Stubs
{
    public class DeadlineStub
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public long DeadLineUnix { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
