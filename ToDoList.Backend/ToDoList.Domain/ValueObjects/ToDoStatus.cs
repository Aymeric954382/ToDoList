using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.ToDo.ValueObjects
{
    public enum ToDoStatus
    {
        Active,
        Completed,
        Expired,
        ExpiringSoon,
        Cancelled
    }

}
