using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
