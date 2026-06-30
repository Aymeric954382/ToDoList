using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Model
{
    public class ExternalServiceResult
    {
        public bool ExecutionSuccess { get; set; }
        public int? Code { get; set; }
        public string Message { get; set; } = default!;
    }
}
