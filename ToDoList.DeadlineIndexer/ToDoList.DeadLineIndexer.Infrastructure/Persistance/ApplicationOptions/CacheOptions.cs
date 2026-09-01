using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.ApplicationOptions
{
    public class CacheOptions
    {
        public int BatchSize { get; set; }
        public int LeadSeconds { get; set; }
    }
}
