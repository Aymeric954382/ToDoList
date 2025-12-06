using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.Providers;
using ToDoList.Gateway.Infrastructure.Persistance.Security.JWT;

namespace ToDoList.Gateway.Infrastructure.Persistance.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<InternalJwtTokenGenerator>();
            services.AddSingleton<IInternalJwtTokenProvider, InternalJwtTokenProvider>();

            services.AddTransient<JwtAuthorizationHandler>();

            services.AddHttpClient<ITaskManagerApiClient, TaskManagerApiClient>(client =>
            {
                client.BaseAddress = new Uri(config["TaskManagerApi:BaseUrl"]);
            })
                        .AddHttpMessageHandler<JwtAuthorizationHandler>();

            services.AddHttpClient<ITaskStateManagerApiClient, TaskStateManagerApiClient>(client =>
            {
                client.BaseAddress = new Uri(config["TaskStateManagerApi:BaseUrl"]);
            })
            .AddHttpMessageHandler<JwtAuthorizationHandler>();


            return services;
        }
    }
}
