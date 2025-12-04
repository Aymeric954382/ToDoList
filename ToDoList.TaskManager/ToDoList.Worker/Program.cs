using ToDoList.Application.Interfaces.Repository;
using ToDoList.Infrastructure.Persistance.DataBaseCommon.EF;

namespace ToDoList.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<IToDoRepository, ToDoRepository>();
                    services.AddHostedService<ToDoExpirationWorker>();
                })
                .Build()
                .Run();
        }
    }
}