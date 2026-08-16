using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.gRPC
{
    public class GRPCOptions
    {
        public required string ApplicationKey { get; init; }
    }
}
