using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models.ResponseDto.Change
{
    public class ChangeToDoPriorityResponse
    {
        public Guid Id { get; set; }
    }
}
