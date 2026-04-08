using ToDoList.TaskStateService.Application.Features.ToDoItems.Filter;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Interfaces.Repository
{
    public interface IToDoRepository
    {
        Task<List<ToDoItem>> GetByFilterAsync(ToDoFilter filter, CancellationToken cancellationToken);
        Task<ToDoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ToDoItem>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task AddAsync(ToDoItem todo, CancellationToken cancellationToken);
        Task UpdateAsync(ToDoItem todo, CancellationToken cancellationToken);
        Task DeleteAsync(ToDoItem todo, CancellationToken cancellationToken);
    }
}
