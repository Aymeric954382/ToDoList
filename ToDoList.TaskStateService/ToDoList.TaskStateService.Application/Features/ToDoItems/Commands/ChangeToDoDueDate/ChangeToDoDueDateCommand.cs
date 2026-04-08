using AutoMapper;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Application.Interfaces.MappingMark;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommand 
        : IWithResultCommand<ServiceResult<ChangeToDoDueDateResponseDto>>, 
            IMapWith<ChangeToDoDueDateResponseDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoDueDateResponseDto, ChangeToDoDueDateCommand>();
        }
    }
}
