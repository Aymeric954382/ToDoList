using ToDoList.StateUpdater.Contracts.ApiClients.RequestDto;
using ToDoList.StateUpdater.Contracts.ApiClients.ResponseDto;

namespace ToDoList.StateUpdater.Contracts.ApiClients.Interfaces
{
    public interface ITaskStateClientApiClient
    {
        Task<ServiceApiResponse<UpdateToDoDeadLinesResponseDto>> UpdateDeadLines(
            UpdateToDoDeadLinesRequestDto dto, 
            CancellationToken cancellationToken);
    }
}
