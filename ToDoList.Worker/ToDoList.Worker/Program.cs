using ToDoList.Worker.Application.DI;
using ToDoList.Worker.Infrastructure.DI;
namespace ToDoList.Worker.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddHostedService<Worker>();
            builder.Services.AddApplication();
            builder.Services.AddPersistance(builder.Configuration);

            var host = builder.Build();
            host.Run();
        }
    }
}