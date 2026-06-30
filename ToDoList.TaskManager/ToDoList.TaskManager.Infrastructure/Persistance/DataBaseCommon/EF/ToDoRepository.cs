using Microsoft.EntityFrameworkCore;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Infrastructure.Persistance.DataBaseCommon.EF
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

        public void Add(ToDoItem todo)
        {
            _context.ToDoItems.Add(todo);
        }

        public void Update(ToDoItem todo)
        {
            _context.ToDoItems.Update(todo);
        }

        public void Delete(ToDoItem todo)
        {
            _context.ToDoItems.Remove(todo);
        }
    }
}
