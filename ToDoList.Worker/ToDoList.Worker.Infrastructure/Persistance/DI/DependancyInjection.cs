using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces.Repository;
using ToDoList.Worker.Application.Interfaces;
using ToDoList.Worker.Infrastructure.Persistance.DI.DataBaseCommon.EF;

namespace ToDoList.Worker.Infrastructure.Persistance.DI
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

