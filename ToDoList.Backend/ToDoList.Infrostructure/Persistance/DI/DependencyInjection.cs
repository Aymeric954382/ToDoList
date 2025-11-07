using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Infrastructure.Persistance.DataBaseCommon.EF;

namespace ToDoList.Infrastructure.Persistance.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ToDoDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IToDoDbContext, ToDoDbContext>(provider =>
                provider.GetService<ToDoDbContext>());

            services.AddScoped<IToDoRepository, ToDoRepository>();

            return services;
        }
    }
}
