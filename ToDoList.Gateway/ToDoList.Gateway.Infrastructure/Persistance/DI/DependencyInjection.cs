using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;
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

            services.AddHttpClient<ITaskStateClientApiClient, TaskStateServiceApiClient>(client =>
            {
                client.BaseAddress = new Uri(config["TaskStateManagerApi:BaseUrl"]);
            })
            .AddHttpMessageHandler<JwtAuthorizationHandler>();

            services.Configure<TaskStateServiceApiOptions>(
                config.GetSection("TaskStateServiceApi")
            );

            services.Configure<TaskManagerApiOptions>(
                config.GetSection("TaskManagerApi")
            );

            return services;
        }
    }
}
