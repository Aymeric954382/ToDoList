using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Filter
{
    public class ToDoFilter
    {
        public Guid UserId { get; set; }

        public ToDoStatus? Status { get; set; }
        public ToDoPriority? Priority { get; set; }

        public bool? IsOverdue { get; set; }

        public DateTime? DueBefore { get; set; }
        public DateTime? DueAfter { get; set; }
    }
}
