using Microsoft.EntityFrameworkCore;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.Interfaces
{
    public interface IToDoDbContext
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellatioToken);
    }
}
