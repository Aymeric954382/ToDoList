using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Application.Common.Exceptions
{
    public class IdenticalReplacementException : Exception
    {
        public IdenticalReplacementException(string name, object replaceValue, object key)
            : base($"Entity {name} already has a meaning - ({replaceValue}) on key ({key})") { }
    }
}
