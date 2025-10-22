using ToDoList.Identity.Application.DI;
using ToDoList.Identity.Infrastructure.Configuration;
using ToDoList.Identity.Infrastructure.Data;

namespace ToDoList.Identity.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentityConfiguration(builder.Configuration);
            builder.Services.AddControllersWithViews();
            builder.Services.AddApplication();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var servicesProvider = scope.ServiceProvider;
                try
                {
                    var context = servicesProvider.GetRequiredService<AuthDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {
                    var logger = servicesProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "An error occurred while app initialization");
                }
            }

            app.UseRouting();
            app.UseIdentityServer();
            app.MapDefaultControllerRoute();


            app.Run();
        }
    }
}
