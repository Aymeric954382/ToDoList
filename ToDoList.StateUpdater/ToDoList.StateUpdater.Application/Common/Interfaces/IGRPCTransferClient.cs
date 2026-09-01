using ToDoList.StateUpdater.Contracts;

namespace ToDoList.StateUpdater.Application.Common.Interfaces
{
    public interface IGRPCTransferClient
    {
        Task SendUpdateAsync(
            List<DeadlineUpdateDto> stubs,
            CancellationToken cancellationToken);
    }
}
