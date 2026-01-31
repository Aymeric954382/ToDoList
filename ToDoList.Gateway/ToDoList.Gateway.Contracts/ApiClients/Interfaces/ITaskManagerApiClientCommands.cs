using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskManagerApiClientCommands
    {
        Task<GetToDoIdDto> CreateAsync(CreateForManagerToDoDto dto);
        Task<HttpResponseMessage> DeleteAsync(GetToDoListOverdueDto dto);
        Task<HttpResponseMessage> ChangeContentAsync(ChangeToDoContentDto dto);

    }
}
