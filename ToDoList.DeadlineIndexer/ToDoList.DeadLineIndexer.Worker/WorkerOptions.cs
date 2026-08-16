using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Worker
{
    public class WorkerOptions
    {
        public required int IntervalTimeMinutes { get; init; }
    }
}
