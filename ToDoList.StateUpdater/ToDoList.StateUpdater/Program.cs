namespace ToDoList.StateUpdater.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<DueProcessorService>();

            var host = builder.Build();
            host.Run();
        }
    }
}