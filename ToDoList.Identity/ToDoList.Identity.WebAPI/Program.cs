using ToDoList.Identity.Application.DI;
using ToDoList.Identity.Infrastructure.Configuration;
using ToDoList.Identity.Infrastructure.Data;
using ToDoList.Identity.WebAPI.Middleware;

namespace ToDoList.Identity.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentityConfiguration(builder.Configuration);
            builder.Services.AddApplication();

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("Frontend will be ready soon") //Frontend will be ready soon
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

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

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.MapControllers();


            app.Run();
        }
    }
}
