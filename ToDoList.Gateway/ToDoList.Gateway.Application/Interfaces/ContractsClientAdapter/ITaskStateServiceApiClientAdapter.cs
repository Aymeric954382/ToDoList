using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoDueDate;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetByPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetByStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetOverdueToDo;
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
        public Task ChangeDueDateAsync(ChangeToDoDueDateCommand command, 
            CancellationToken cancellationToken);
        public Task ChangeStatusAsync(ChangeToDoStatusCommand command, 
            CancellationToken cancellationToken);
        public Task ChangePriorityAsync(ChangeToDoPriorityCommand command, 
            CancellationToken cancellationToken);
        public Task CreateAsync(CreateToDoCommand command, Guid id, 
            CancellationToken cancellationToken);
        public Task DeleteAsync(DeleteToDoCommand command, 
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
