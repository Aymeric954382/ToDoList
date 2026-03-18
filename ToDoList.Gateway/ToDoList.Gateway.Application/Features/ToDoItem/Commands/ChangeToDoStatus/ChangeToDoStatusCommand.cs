using AutoMapper;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.ValueObjects;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommand 
        : IWithResultCommand<ServiceResult<TaskStateServiceChangeStatusResponseDto>>, 
            IMapWith<TaskStateServiceChangeStatusRequestDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoStatusCommand, TaskStateServiceChangeStatusRequestDto>();
        }
    }
}
