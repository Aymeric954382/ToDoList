using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Filter;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.DataBaseCommon.EF
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

        public async Task<List<ToDoItem>> GetListByUserIdAsync(Guid userId, CancellationToken cancellationToken)
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

        public async Task<List<ToDoItem>> GetByFilterAsync(
            ToDoFilter filter,
            CancellationToken cancellationToken)
        {
            var query = _context.ToDoItems
                .Where(x => x.UserId == filter.UserId);

            if (filter.Status.HasValue)
                query = query.Where(x => x.Status == filter.Status);

            if (filter.Priority.HasValue)
                query = query.Where(x => x.Priority == filter.Priority);

            if (filter.DueBefore.HasValue)
                query = query.Where(x => x.DueDate <= filter.DueBefore);

            if (filter.DueAfter.HasValue)
                query = query.Where(x => x.DueDate >= filter.DueAfter);

            if (filter.IsOverdue == true)
            {
                var now = DateTime.UtcNow;

                query = query.Where(x =>
                    x.DueDate <= now &&
                    x.Status != ToDoStatus.Completed &&
                    x.Status != ToDoStatus.Cancelled);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}

