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
        public Task<ServiceResult<TaskStateServiceChangeDueDateResponseDto>> ChangeDueDateAsync(ChangeToDoDueDateCommand command, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskStateServiceChangeStatusResponseDto>> ChangeStatusAsync(ChangeToDoStatusCommand command, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskStateServiceChangePriorityResponseDto>> ChangePriorityAsync(ChangeToDoPriorityCommand command, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskStateServiceCreateResponseDto>> CreateAsync(CreateToDoCommand command, Guid id, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskStateServiceDeleteResponseDto>> DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken);

        //query
        public Task<ServiceResult<TaskStateServiceGetToDoListByPriorityResponseDto>> GetToDoListAsync(GetToDoListQuery query, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskStateServiceGetToDoListByPriorityResponseDto>> GetToDoListByPriorityAsync(GetToDoListByPriorityQuery query, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskStateServiceGetToDoListByStatusResponseDto>> GetToDoListByStatusAsync(GetToDoListByStatusQuery query, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskStateServiceGetToDoListByOverdueResponseDto>> GetToDoListByOverdueAsync(GetToDoListByOverdueQuery query, 
            CancellationToken cancellationToken);
        public Task<ServiceResult<TaskStateServiceGetToDoListByIdsResponseDto>> GetToDoListAsync(GetToDoListByIdsRequestQuery query,
            CancellationToken cancellationToken);
    }
}
