using System.ComponentModel.DataAnnotations;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.WebAPI.Models.ResponseDto.Change
{
    public class ChangeToDoStatusResponse
    {
        public Guid Id { get; set; }
    }
}
