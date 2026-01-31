using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.DI.DataBaseCommon.EF.EntityTypeConfiguration
{
    public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

        }
    }
}
