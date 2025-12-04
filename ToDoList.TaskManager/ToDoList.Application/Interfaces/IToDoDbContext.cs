using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.ToDo;

namespace ToDoList.Application.Interfaces
{
    public interface IToDoDbContext
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellatioToken);
    }
}
