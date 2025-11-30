using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Worker.Infrastructure.Persistance.WorkerConfiguration
{
    public class WorkerSettings
    {
        public int IntervalSeconds { get; set; } = 60;
    }

}
