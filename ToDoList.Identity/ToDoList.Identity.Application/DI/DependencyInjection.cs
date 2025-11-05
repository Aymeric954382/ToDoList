using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Identity.Application.Common.Services.JWTTokenGeneration;

namespace ToDoList.Identity.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<JWTTokenGenerator>();
            return services;
        }
    }
}
