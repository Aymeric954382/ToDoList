using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
