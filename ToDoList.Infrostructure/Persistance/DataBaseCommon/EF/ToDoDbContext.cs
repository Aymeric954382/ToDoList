using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.ToDo;
using ToDoList.Infrostructure.Persistance.DataBaseCommon.EF.EntityTypeConfigurations;

namespace ToDoList.Infrastructure.Persistance.DataBaseCommon.EF
{
    public class ToDoDbContext : DbContext, IToDoItemsDbContext
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
