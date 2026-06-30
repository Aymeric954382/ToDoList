using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ToDoList.StateUpdater.Application.Interfaces;
using ToDoList.StateUpdater.Contracts.ApiClients.Routes;
using ToDoList.StateUpdater.Infrastructure.Helpers.HttpExecutor;
using ToDoList.StateUpdater.Infrastructure.Redis;

namespace ToDoList.StateUpdater.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<IRedisClient, RedisClient>();

            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();

                return new X509Certificate2(
                    config["Cert:Path"],
                    config["Cert:Password"]);
            });

            services.Configure<TaskStateApiOptions>(
                config.GetSection("TaskStateServiceApi"));

            services.AddHttpClient<HttpExecutor>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<TaskStateApiOptions>>().Value;

                client.BaseAddress = new Uri(options.BaseUrl);
            });

            return services;
        }
    }
}
