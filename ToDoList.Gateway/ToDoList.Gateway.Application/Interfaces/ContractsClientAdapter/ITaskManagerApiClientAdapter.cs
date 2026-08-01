using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoContent;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries;

namespace ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter
{
    public interface ITaskManagerApiClientAdapter
    {
        //command
        public Task ChangeContentAsync(ChangeToDoContentCommand command, 
            CancellationToken cancellationToken);
        public Task CreateAsync(CreateToDoCommand command,
            Guid id,
            CancellationToken cancellationToken);
        public Task DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken);

        //query
        public Task<TaskManagerGetToDoListResponseDto> GetToDoListAsync(GetToDoListQuery query, 
            CancellationToken cancellationToken);

        public Task<TaskManagerGetToDoListByIdsResponseDto> GetToDoListByIdAsync(GetToDoListByIdsRequestQuery query,
            CancellationToken cancellationToken);

    }
}
