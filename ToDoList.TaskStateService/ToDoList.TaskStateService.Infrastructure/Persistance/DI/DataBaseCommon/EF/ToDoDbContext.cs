using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Infrastructure.Persistance.DI.DataBaseCommon.EF.EntityTypeConfiguration;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.DI.DataBaseCommon.EF
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) :
            base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
