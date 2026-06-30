using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoDueDate;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.ServiceQueries;

namespace ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter
{
    public interface ITaskStateServiceApiClientAdapter
    {
        //command
        public Task<TaskStateServiceChangeDueDateResponseDto> ChangeDueDateAsync(ChangeToDoDueDateCommand command, 
            CancellationToken cancellationToken);
        public Task<TaskStateServiceChangeStatusResponseDto> ChangeStatusAsync(ChangeToDoStatusCommand command, 
            CancellationToken cancellationToken);
        public Task<TaskStateServiceChangePriorityResponseDto> ChangePriorityAsync(ChangeToDoPriorityCommand command, 
            CancellationToken cancellationToken);
        public Task<TaskStateServiceCreateResponseDto> CreateAsync(CreateToDoCommand command, Guid id, 
            CancellationToken cancellationToken);
        public Task<TaskStateServiceDeleteResponseDto> DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken);

        //query
        public Task<TaskStateServiceGetToDoListByPriorityResponseDto> GetToDoListAsync(GetToDoListQuery query, 
            CancellationToken cancellationToken);
        public Task<TaskStateServiceGetToDoListByPriorityResponseDto> GetToDoListByPriorityAsync(GetToDoListByPriorityQuery query, 
            CancellationToken cancellationToken);
        public Task<TaskStateServiceGetToDoListByStatusResponseDto> GetToDoListByStatusAsync(GetToDoListByStatusQuery query, 
            CancellationToken cancellationToken);
        public Task<TaskStateServiceGetToDoListByOverdueResponseDto> GetToDoListByOverdueAsync(GetToDoListByOverdueQuery query, 
            CancellationToken cancellationToken);
        public Task<TaskStateServiceGetToDoListByIdsResponseDto> GetToDoListByIdsAsync(GetToDoListByIdsRequestQuery query,
            CancellationToken cancellationToken);
    }
}
