using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Infrastructure.Persistance.DI.DataBaseCommon.EF;

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

            return services;
        }
    }
}

