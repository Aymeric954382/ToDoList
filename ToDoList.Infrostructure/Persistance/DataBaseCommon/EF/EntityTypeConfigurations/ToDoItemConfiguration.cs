using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.ToDo;

namespace ToDoList.Infrostructure.Persistance.DataBaseCommon.EF.EntityTypeConfigurations
{
    public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.Id).IsUnique();
            builder.Property(i => i.Details).HasMaxLength(100);
            builder.Property(i => i.Title).HasMaxLength(250);
        }
    }
}
