using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;
using ToDoList.Gateway.Application.ToDoItem.Queries.CriteriaSplitter;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetByPriority;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetByStatus;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetOverdueToDo;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter
{
    public interface ITaskManagerApiClientAdapter
    {
        //command
        public Task<HttpResponseMessage> ChangeContentAsync(ChangeToDoContentCommand command);
        public Task<Guid> CreateAsync(CreateToDoCommand command);
        public Task<HttpResponseMessage> DeleteAsync(DeleteToDoCommand command);

        //query
        public Task<ToDoListContainer> GetToDoListAsync(CriteriaSplitterQuery query);

    }
}
