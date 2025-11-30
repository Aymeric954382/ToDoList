using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.WebAPI.Models
{
    public class CreateToDoDto
    {
        [Required]
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
