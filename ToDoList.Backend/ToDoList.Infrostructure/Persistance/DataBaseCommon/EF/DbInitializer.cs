namespace ToDoList.Infrastructure.Persistance.DataBaseCommon.EF
{
    public class DbInitializer
    {
        public static void Initialize(ToDoDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
