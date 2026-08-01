using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.Routes;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.Routes;
using ToDoList.Gateway.Contracts.Interfaces;
using ToDoList.Gateway.Infrastructure.Persistance.Rabbit;
using ToDoList.Gateway.Infrastructure.Persistance.Security.JWT;
using ToDoList.Gateway.Infrastructure.Persistance.Services.TaskManagerApiClient.Queries;
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

            services.AddSingleton<RabbitConnection>();

            services.AddSingleton<MessageSerializer>();
            services.AddSingleton<RabbitMessageFactory>();

            services.AddSingleton<RabbitTopology>();
            services.AddSingleton<RabbitPublisherFactory>();

            services.AddSingleton<RabbitPublisher>();


            return services;
        }
    }
}
