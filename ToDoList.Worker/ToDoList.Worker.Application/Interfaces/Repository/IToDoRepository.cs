using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Domain;

namespace ToDoList.Worker.Application.Repository
{
    public interface IToDoRepository
    {
        IQueryable<ToDoItem> AsQueryable();
        Task<ToDoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ToDoItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task AddAsync(ToDoItem todo, CancellationToken cancellationToken);
        Task UpdateAsync(ToDoItem todo, CancellationToken cancellationToken);
        Task DeleteAsync(ToDoItem todo, CancellationToken cancellationToken);
    }
}
