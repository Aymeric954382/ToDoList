using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Command_QuerySpliter;

namespace ToDoList.Application.ToDoItems.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommand : IVoidCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
