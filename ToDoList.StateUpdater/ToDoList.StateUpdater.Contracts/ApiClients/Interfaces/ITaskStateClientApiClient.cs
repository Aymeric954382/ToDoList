using ToDoList.StateUpdater.Contracts.ApiClients.RequestDto;

namespace ToDoList.StateUpdater.Contracts.ApiClients.Interfaces
{
    public interface ITaskStateClientApiClient
    {
        Task UpdateDeadLines(UpdateToDoDeadLinesRequestDto dto, CancellationToken cancellationToken);
    }
}
