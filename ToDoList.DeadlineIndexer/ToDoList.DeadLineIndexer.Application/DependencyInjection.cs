using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DeadlineIndexer.Application.CacheService;
using ToDoList.DeadlineIndexer.Application.TransferService;


namespace ToDoList.DeadlineIndexer.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<CacheExtractor>();
            services.AddScoped<DeadlineProcessor>();

            return services;
        }
    }
}
