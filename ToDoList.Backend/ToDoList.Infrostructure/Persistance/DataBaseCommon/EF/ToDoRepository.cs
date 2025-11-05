using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Domain.ToDo;

namespace ToDoList.Infrastructure.Persistance.DataBaseCommon.EF
{
    public class ToDoRepository : IToDoRepository
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
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
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                _context.ToDoItems.Update(todo);
                await _context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }
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
