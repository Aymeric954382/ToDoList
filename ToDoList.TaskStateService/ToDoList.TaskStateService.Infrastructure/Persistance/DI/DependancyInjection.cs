using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Application.Interfaces.Redis;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Infrastructure.Persistance.DataBaseCommon.EF;
using ToDoList.TaskStateService.Infrastructure.Persistance.Redis;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.DI
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnectionString"];
            services.AddDbContext<ToDoDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IToDoDbContext, ToDoDbContext>(provider =>
                provider.GetService<ToDoDbContext>());

            services.AddScoped<IToDoRepository, ToDoRepository>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var config = configuration.GetConnectionString("Redis");

                var options = ConfigurationOptions.Parse(config);
                options.AbortOnConnectFail = false;

                return ConnectionMultiplexer.Connect(options);
            });

            services.AddSingleton<IDeadLineQueue, DeadLineQueue>();

            return services;
        }
    }
}

