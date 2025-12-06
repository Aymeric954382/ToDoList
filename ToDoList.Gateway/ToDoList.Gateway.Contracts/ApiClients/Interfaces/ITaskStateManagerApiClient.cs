using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Contracts.ApiClients.Interfaces
{
    public interface ITaskStateManagerApiClient
    {
        Task<ToDoResponseDto> CreateAsync(CreateToDoDto dto);
        Task<ToDoResponseDto> DeleteAsync(DeleteToDoDto dto);
        Task<ToDoResponseDto> ChangePriorityAsync(ChangeToDoPriorityDto dto);
        Task<ToDoResponseDto> ChangeStatusAsync(ChangeToDoStatusDto dto);
        Task<ToDoResponseDto> ChangeDueDateAsync(ChangeToDoDueDateDto dto);

    }
}
