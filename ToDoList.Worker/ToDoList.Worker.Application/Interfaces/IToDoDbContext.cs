using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Domain;

namespace ToDoList.Worker.Application.Interfaces
{
    public interface IToDoDbContext
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellatioToken);
    }
}
