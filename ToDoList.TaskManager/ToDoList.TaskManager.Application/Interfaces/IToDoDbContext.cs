using Microsoft.EntityFrameworkCore;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.Interfaces
{
    public interface IToDoDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellatioToken);
    }
}
