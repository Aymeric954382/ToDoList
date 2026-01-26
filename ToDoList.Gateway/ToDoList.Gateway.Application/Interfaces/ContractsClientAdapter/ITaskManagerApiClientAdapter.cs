using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter
{
    public interface ITaskManagerApiClientAdapter
    {
        public Task<HttpResponseMessage> ChangeContentAsync(ChangeToDoContentCommand command);
        public Task<Guid> CreateAsync(CreateToDoCommand command);
        public Task<HttpResponseMessage> DeleteAsync(DeleteToDoCommand command);

    }
}
