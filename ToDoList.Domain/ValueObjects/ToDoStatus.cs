using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.ValueObjects
{
    public class ToDoStatus
    {
        public enum TodoStatus
        {
            Active,       
            Completed,     
            Expired,       
            ExpiringSoon,  
            Cancelled      
        }
    }
}
