using AutoMapper;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoDueDate;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Application.Interfaces.MappingMark;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommand 
        : IWithResultCommand<ServiceResult<DeleteToDoResponseDto>>, 
            IMapWith<DeleteToDoResponseDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteToDoResponseDto, DeleteToDoCommand>();
        }
    }
}
