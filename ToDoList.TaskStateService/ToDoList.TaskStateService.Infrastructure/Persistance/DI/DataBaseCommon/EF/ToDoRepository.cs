using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.DI.DataBaseCommon.EF
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;
        public ToDoRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public async Task<ToDoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _context.ToDoItems.FindAsync(id);

        public async Task<List<ToDoItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => await _context.ToDoItems.Where(t => t.UserId == userId).ToListAsync();

        public async Task AddAsync(ToDoItem todo, CancellationToken cancellationToken)
        {
            await _context.ToDoItems.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDoItem todo, CancellationToken cancellationToken)
        {
            _context.ToDoItems.Update(todo);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ToDoItem todo, CancellationToken cancellationToken)
        {
            _context.ToDoItems.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public IQueryable<ToDoItem> AsQueryable()
        {
            return _context.ToDoItems.AsNoTracking();
        }
    }
}

