using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models.ResponseDto.Create
{
    public class CreateToDoResponse
    {
        public Guid Id { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
