using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StackExchange.Redis;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Contracts.ApiClients;
using ToDoList.DeadlineIndexer.Contracts.Protos;
using ToDoList.DeadlineIndexer.Infrastructure.Persistance.gRPC;
using ToDoList.DeadlineIndexer.Infrastructure.Persistance.Mongo;
using ToDoList.DeadlineIndexer.Infrastructure.Persistance.Redis;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(options => configuration.GetSection(MongoOptions.SectionName).Bind(options));
            services.Configure<RedisOptions>(options => configuration.GetSection("RedisSettings").Bind(options));
            services.Configure<GRPCOptions>(options => configuration.GetSection("gRPCSettings").Bind(options));

            services.Configure<CollectionConfigurations>(options => configuration.GetSection("CollectionConfigurations").Bind(options));

            services.AddSingleton<CollectionConfigurations>(sp => sp.GetRequiredService<IOptions<CollectionConfigurations>>().Value);

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisOptions = sp.GetRequiredService<IOptions<RedisOptions>>().Value;

                var config = new ConfigurationOptions
                {
                    EndPoints = { redisOptions.Endpoints },
                    Password = redisOptions.Password,
                    Ssl = redisOptions.SslCert,
                    AbortOnConnectFail = redisOptions.AbortOnConnectFail
                };

                return ConnectionMultiplexer.Connect(config);
            });

            services.AddScoped<IDatabase>(sp =>
            {
                var multiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
                return multiplexer.GetDatabase();
            });

            services.AddSingleton<RedisConnectionFactory>();
            services.AddScoped<IRedisCacheRepository, RedisCacheRepository>();

            services.AddSingleton<IMongoClient>(sp =>
            {
                var mongoOptions = sp.GetRequiredService<IOptions<MongoOptions>>().Value;

                return new MongoClient(mongoOptions.ConnectionString);
            });

            services.AddSingleton<MongoConnectionFactory>();
            services.AddScoped<IDeadlineDbRepository, MongoCacheRepository>();
            services.AddTransient<MongoInitializer>();

            services.AddGrpcClient<DeadlineTransferService.DeadlineTransferServiceClient>((sp, o) =>
            {
                var options = sp.GetRequiredService<IOptions<TaskStateUpdaterRoutes>>().Value;

                o.Address = new Uri(new Uri(options.BaseAddress), options.UpdateAll);
            });

            services.AddScoped<IDeadlineNotificationClient, GRPCTransferClient>();

            return services;
        }
    }
}
