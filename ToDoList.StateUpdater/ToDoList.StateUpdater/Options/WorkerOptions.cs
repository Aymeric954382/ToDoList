using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Worker.Options
{
    public class WorkerOptions
    {
        public required int MaxBatchSize { get; init; }
    }
}
