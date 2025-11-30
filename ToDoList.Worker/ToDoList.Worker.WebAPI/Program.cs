using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Threading.Tasks;
using ToDoList.Worker.Application.DI;
using ToDoList.Worker.Application.Interfaces;
using ToDoList.Worker.Infrastructure.Persistance.DI;
using ToDoList.Worker.Infrastructure.Persistance.DI.DataBaseCommon.EF;
using ToDoList.Worker.Infrastructure.Persistance.WorkerConfiguration;

namespace ToDoList.Worker.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<WorkerSettings>(
            builder.Configuration.GetSection("WorkerSettings"));

            builder.Services.AddApplication();
            builder.Services.AddPersistance(builder.Configuration);

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(
                Assembly.GetExecutingAssembly(),
                typeof(IToDoDbContext).Assembly
            );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowGatewayAPI", policy =>
                {
                    policy.WithOrigins("GatewayAPI will be ready soon")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowGatewayAPI");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var servicesProvider = scope.ServiceProvider;
                try
                {
                    var context = servicesProvider.GetRequiredService<ToDoDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {
                    //need Ex
                }
            }

           await app.RunAsync();
        }
    }
}