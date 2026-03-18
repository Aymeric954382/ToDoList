using AutoMapper;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.ValueObjects;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo
{
    public class CreateToDoCommand 
        : IWithResultCommand<ServiceResult<CreateToDoResponseDto>>, 
            IMapWithTwo<TaskManagerCreateRequestDto, TaskStateServiceCreateRequestDto>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateToDoCommand, TaskManagerCreateRequestDto>();
            profile.CreateMap<CreateToDoCommand, TaskStateServiceCreateRequestDto>();
        }
    }
}
