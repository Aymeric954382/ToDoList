using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces.Repository;
using ToDoList.Worker.Domain;

namespace ToDoList.Worker.Infrastructure.DataBaseCommon.EF
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

