using AutoMapper;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Delete;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoCommand 
        : IWithResultCommand<ServiceResult<DeleteToDoResponseDto>>, 
            IMapWith<TaskStateServiceDeleteRequestDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteToDoCommand, TaskStateServiceDeleteRequestDto>();
        }
    }
}
