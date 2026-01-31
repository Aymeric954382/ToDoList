using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;

namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskStateServiceApiClientCommands
    {
        Task<HttpResponseMessage> CreateAsync(CreateForServiceToDoDto dto);
        Task<HttpResponseMessage> DeleteAsync(GetToDoListOverdueDto dto);
        Task<HttpResponseMessage> ChangePriorityAsync(ChangeToDoPriorityDto dto);
        Task<HttpResponseMessage> ChangeStatusAsync(ChangeToDoStatusDto dto);
        Task<HttpResponseMessage> ChangeDueDateAsync(ChangeToDoDueDateDto dto);

    }
}
