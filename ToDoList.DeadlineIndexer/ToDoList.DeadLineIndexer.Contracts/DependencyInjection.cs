using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DeadlineIndexer.Contracts.ApiClients;

namespace ToDoList.DeadLineIndexer.Contracts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContracts(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<TaskStateUpdaterRoutes>(options => configuration.GetSection("TaskStateUpdaterRoutes").Bind(options));

            return services;
        }
    }
}
