using ToDoList.TaskStateService.Application.Features.ToDoItems.Filter;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Interfaces.Repository
{
    public interface IToDoRepository
    {
        Task<List<ToDoItem>> GetByFilterAsync(ToDoFilter filter, CancellationToken cancellationToken);
        Task<ToDoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ToDoItem>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        Task<List<ToDoItem>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        void Add(ToDoItem todo);
        void Update(ToDoItem todo);
        void Delete(ToDoItem todo);
    }
}
