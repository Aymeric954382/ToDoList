using AutoMapper;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommand 
        : IWithResultCommand<ServiceResult<TaskStateServiceChangeDueDateResponseDto>>, 
            IMapWith<TaskStateServiceChangeDueDateRequestDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoDueDateCommand, TaskStateServiceChangeDueDateRequestDto>();
        }
    }
}
