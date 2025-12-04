using System.Reflection;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Infrastructure.Persistance.DI.DataBaseCommon.EF;
using ToDoList.TaskStateService.Application.DI;
using ToDoList.TaskStateService.Infrastructure.Persistance.DI;
using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.TaskStateService.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplication();
            builder.Services.AddPersistance(builder.Configuration);

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(
                Assembly.GetExecutingAssembly(),
                typeof(IToDoDbContext).Assembly
            );

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });


            string redisConnection = builder.Configuration.GetConnectionString("Redis");

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(redisConnection);
            });

            var app = builder.Build();

            app.UseRouting();
            app.UseHttpsRedirection();
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