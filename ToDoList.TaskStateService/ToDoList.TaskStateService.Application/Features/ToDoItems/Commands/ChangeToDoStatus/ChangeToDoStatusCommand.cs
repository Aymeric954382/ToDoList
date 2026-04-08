using AutoMapper;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Application.Interfaces.MappingMark;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommand 
        : IWithResultCommand<ServiceResult<ChangeToDoStatusResponseDto>>, 
            IMapWith<ChangeToDoStatusResponseDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoStatusResponseDto, ChangeToDoStatusCommand>();
        }
    }
}
