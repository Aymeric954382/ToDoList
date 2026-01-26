using System.Net.Http;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskManagerApiClient
    {
        Task<ToDoResponseDto> CreateAsync(CreateForManagerToDoDto dto);
        Task<HttpResponseMessage> DeleteAsync(DeleteToDoDto dto);
        Task<HttpResponseMessage> ChangeContentAsync(ChangeToDoContentDto dto);

    }
}
