using Microsoft.EntityFrameworkCore;
using ToDoList.TaskManager.Application.Interfaces;
using ToDoList.TaskManager.Domain;
using ToDoList.TaskManager.Infrastructure.Persistance.DataBaseCommon.EF.EntityTypeConfigurations;

namespace ToDoList.TaskManager.Infrastructure.Persistance.DataBaseCommon.EF
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
