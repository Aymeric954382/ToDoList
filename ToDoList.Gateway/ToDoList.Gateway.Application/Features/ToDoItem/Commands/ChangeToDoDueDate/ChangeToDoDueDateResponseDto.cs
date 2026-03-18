using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
