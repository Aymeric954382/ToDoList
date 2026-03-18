using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.ValueObjects;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.DomainResponseDtos
{
    public class ToDoItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
