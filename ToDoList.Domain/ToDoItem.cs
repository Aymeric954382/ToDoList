using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain
{
    public class ToDoItem
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Details { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime DateUpdate { get; private set; }
        public ToDoStatus Status { get; private set; }
    }
}
