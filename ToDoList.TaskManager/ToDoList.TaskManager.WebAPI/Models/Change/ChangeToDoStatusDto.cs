using System.ComponentModel.DataAnnotations;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.WebAPI.Models.Change
{
    public class ChangeToDoStatusDto
    {
        [Required]
        public Guid Id { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
