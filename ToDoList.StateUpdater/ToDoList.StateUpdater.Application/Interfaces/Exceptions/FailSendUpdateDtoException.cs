using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ToDoList.StateUpdater.Application.Interfaces.Exceptions
{
    public class FailSendUpdateDtoException : Exception
    {
        public FailSendUpdateDtoException() 
            : base($"Send UpdateDueDateDto failed") { }
    }
}
