namespace ToDoList.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<ToDoExpirationWorker>();

            var host = builder.Build();
            host.Run();
        }
    }
}