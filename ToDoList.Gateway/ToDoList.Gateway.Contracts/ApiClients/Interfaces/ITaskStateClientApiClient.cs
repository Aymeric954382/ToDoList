using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskStateServiceApiClient
    {
        Task<HttpResponseMessage> CreateAsync(CreateForServiceToDoDto dto);
        Task<HttpResponseMessage> DeleteAsync(DeleteToDoDto dto);
        Task<HttpResponseMessage> ChangePriorityAsync(ChangeToDoPriorityDto dto);
        Task<HttpResponseMessage> ChangeStatusAsync(ChangeToDoStatusDto dto);
        Task<HttpResponseMessage> ChangeDueDateAsync(ChangeToDoDueDateDto dto);

    }
}
