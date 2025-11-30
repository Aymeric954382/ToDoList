using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.WebAPI.Models
{
    public class ChangeToDoStatusDto
    {
        [Required]
        public Guid Id { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
