using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.Interfaces.Repository
{
    public interface IToDoRepository
    {
        Task<ToDoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ToDoItem>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        void Add(ToDoItem todo);
        void Update(ToDoItem todo);
        void Delete(ToDoItem todo);
    }
}
