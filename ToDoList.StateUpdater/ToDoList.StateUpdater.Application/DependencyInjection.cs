using Microsoft.Extensions.DependencyInjection;
using ToDoList.StateUpdater.Application.Services;
using ToDoList.StateUpdater.Contracts.Interfaces;

namespace ToDoList.StateUpdater.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IUpdateFillter, UpdateFilter>();
            services.AddSingleton<DeadlineInMemoryQueue>();

            return services;
        }
    }
}
