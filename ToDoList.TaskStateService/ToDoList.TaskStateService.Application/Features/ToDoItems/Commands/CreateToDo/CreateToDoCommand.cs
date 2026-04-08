using AutoMapper;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Application.Interfaces.MappingMark;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoCommand 
        : IWithResultCommand<ServiceResult<CreateToDoResponseDto>>, 
            IMapWith<CreateToDoResponseDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateToDoResponseDto, CreateToDoCommand>();
        }
    }


}
