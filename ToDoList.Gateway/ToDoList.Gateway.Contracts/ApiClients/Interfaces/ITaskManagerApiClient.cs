using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;
namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskManagerApiClient
    {
        Task<ToDoResponseDto> CreateAsync(CreateToDoDto dto);
        Task<ToDoResponseDto> DeleteAsync(DeleteToDoDto dto);
        Task<ToDoResponseDto> ChangeContentAsync(ChangeToDoContentDto dto);

    }
}
