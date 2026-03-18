using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.ValueObjects;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
