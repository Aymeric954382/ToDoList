using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Gateway.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(IServiceCollection services)
        {
            return services;
        }
    }
}
