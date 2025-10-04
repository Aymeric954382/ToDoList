using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.ToDo;
using ToDoList.Infrostructure.Persistance.DataBaseCommon.EF.EntityTypeConfigurations;

namespace ToDoList.Infrostructure.Persistance.DataBaseCommon.EF
{
    public class ToDoItemsDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public ToDoItemsDbContext(DbContextOptions<ToDoItemsDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
