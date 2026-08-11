using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.TaskManager.Application.Interfaces;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Infrastructure.Persistance.DataBaseCommon.EF;
using ToDoList.TaskManager.Infrastructure.Persistance.Rabbit;

namespace ToDoList.TaskManager.Infrastructure.Persistance.DI
{
    public static class DependencyInjection
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

            services.AddSingleton<RabbitOperationCatalog>();
            services.AddSingleton<RabbitConnection>();

            return services;
        }
    }
}
