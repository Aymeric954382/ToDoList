using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces;
using ToDoList.Worker.Domain;
using ToDoList.Worker.Infrastructure.Persistance.DI.DataBaseCommon.EF.EntityTypeConfiguration;

namespace ToDoList.Worker.Infrastructure.Persistance.DI.DataBaseCommon.EF
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
