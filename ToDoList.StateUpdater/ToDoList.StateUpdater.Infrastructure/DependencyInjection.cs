using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.StateUpdater.Infrastructure.gRPC.Clients;
using ToDoList.StateUpdater.Infrastructure.gRPC.Options;
using ToDoList.StateUpdater.Contracts.Protos.Updater;
using Microsoft.Extensions.Options;
using ToDoList.StateUpdater.Contracts.ApiClients;
using ToDoList.StateUpdater.Application.Common.Interfaces;

namespace ToDoList.StateUpdater.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddGrpc();
            services.Configure<GRPCServerOptions>(config.GetSection("GRPCServerOptions"));
            services.Configure<GRPCClientOptions>(config.GetSection("GRPCClientOptions"));

            services.AddGrpcClient<DeadlineTransferService.DeadlineTransferServiceClient>((sp, o) =>
            {
                var options = sp.GetRequiredService<IOptions<TaskStateServiceRoutes>>().Value;

                o.Address = new Uri(new Uri(options.BaseAddress), options.UpdateAll);
            });

            services.AddScoped<IGRPCTransferClient, GRPCTransferClient>();

            return services;
        }
    }
}
