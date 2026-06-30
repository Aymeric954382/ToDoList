using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Gateway.Application.Common.Mappings.Helpers;
using ToDoList.Gateway.Application.Common.Mappings.Profiles;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;
using ToDoList.Gateway.Contracts.Helpers;
using ToDoList.Gateway.Contracts.Interfaces;
using ToDoList.Gateway.Infrastructure.Persistance.Security.JWT;
using ToDoList.Gateway.Infrastructure.Persistance.Services.TaskManagerApiClient.Commands;
using ToDoList.Gateway.Infrastructure.Persistance.Services.TaskManagerApiClient.Queries;
using ToDoList.Gateway.Infrastructure.Persistance.Services.TaskStateServiceApiClient.Commands;
using ToDoList.Gateway.Infrastructure.Persistance.Services.TaskStateServiceApiClient.Queries;

namespace ToDoList.Gateway.Infrastructure.Persistance.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<InternalJwtTokenGenerator>();
            services.AddSingleton<InternalJwtTokenProvider>();

            services.AddTransient<JwtAuthorizationHandler>();

            services.AddHttpClient<ITaskManagerApiClientCommands, TaskManagerApiClientCommands>(client =>
            {
                client.BaseAddress = new Uri(config["TaskManagerApi:BaseUrl"]);
            })
            .AddHttpMessageHandler<JwtAuthorizationHandler>();

            services.AddHttpClient<ITaskStateServiceApiClientCommands, TaskStateServiceApiClientCommands>(client =>
            {
                client.BaseAddress = new Uri(config["TaskStateServiceApi:BaseUrl"]);
            })
            .AddHttpMessageHandler<JwtAuthorizationHandler>();

            services.AddHttpClient<ITaskManagerApiClientQueries, TaskManagerApiClientQueries>(client =>
            {
                client.BaseAddress = new Uri(config["TaskManagerApi:BaseUrl"]);
            })
            .AddHttpMessageHandler<JwtAuthorizationHandler>();

            services.AddHttpClient<ITaskStateServiceApiClientQueries, TaskStateServiceApiClientQueries>(client =>
            {
                client.BaseAddress = new Uri(config["TaskStateServiceApi:BaseUrl"]);
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
