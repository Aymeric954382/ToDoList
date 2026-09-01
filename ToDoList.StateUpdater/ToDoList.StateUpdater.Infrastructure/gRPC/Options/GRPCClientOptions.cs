using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Infrastructure.gRPC.Options
{
    public class GRPCClientOptions
    {
        public required string ApplicationKey { get; init; }
    }
}
