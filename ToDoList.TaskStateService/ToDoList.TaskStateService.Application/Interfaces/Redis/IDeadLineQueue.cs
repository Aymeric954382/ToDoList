using ToDoList.TaskStateService.Application.Common.Stubs;

namespace ToDoList.TaskStateService.Application.Interfaces.Redis
{
    public interface IDeadLineQueue
    {
        Task AddDeadlineStubAsync(DeadLineStub stub);
    }
}
