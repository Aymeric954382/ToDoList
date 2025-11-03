
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Domain.ToDo;
using ToDoList.Tests.Common.Extensions;

namespace ToDoList.Tests.Common.InMemoryRepository
{
    public class InMemoryToDoRepository : IToDoRepository
    {
        private readonly List<ToDoItem> _items = new();
        public Task AddAsync(ToDoItem todo, CancellationToken cancellationToken = default)
        {
            _items.Add(todo);
            return Task.CompletedTask;
        }

        public IQueryable<ToDoItem> AsQueryable() =>
            _items.AsQueryable().AsAsyncEnumerable();


        public Task DeleteAsync(ToDoItem todo, CancellationToken cancellationToken = default)
        {
            _items.Remove(todo);
            return Task.CompletedTask;
        }

        public Task<ToDoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        public Task<List<ToDoItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
            Task.FromResult(_items.Where(x => x.UserId == userId).ToList());

        public Task UpdateAsync(ToDoItem todo, CancellationToken cancellationToken = default)
        {
            var index = _items.FindIndex(x => x.Id == todo.Id);
            if (index >= 0)
                _items[index] = todo;
            return Task.CompletedTask;
        }
    }
}
