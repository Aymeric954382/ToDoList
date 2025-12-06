using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Infrastructure.Persistance.DataBaseCommon.EF.EntityTypeConfigurations
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
