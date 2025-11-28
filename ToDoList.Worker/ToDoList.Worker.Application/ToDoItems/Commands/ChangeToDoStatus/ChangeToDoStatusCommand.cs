using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.Application.ToDoItems.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
