using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Interfaces
{
    public interface IToDoDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellatioToken);
    }
}
