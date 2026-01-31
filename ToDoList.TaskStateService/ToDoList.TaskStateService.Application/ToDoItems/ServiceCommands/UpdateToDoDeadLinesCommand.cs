using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.TaskStateService.Application.ToDoItems.ServiceCommands
{
    public class UpdateToDoDeadLinesCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

    }
}
