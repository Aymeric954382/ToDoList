using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Identity.Domain.Entities;
using ToDoList.Identity.Infrastructure.Data;

namespace ToDoList.Identity.Infrastructure.Configuration
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
            .AddAspNetIdentity<User>()
            .AddInMemoryApiResources(AuthServerConfiguration.ApiResources)
            .AddInMemoryIdentityResources(AuthServerConfiguration.IdentityResources)
            .AddInMemoryApiScopes(AuthServerConfiguration.ApiScopes)
            .AddInMemoryClients(AuthServerConfiguration.Clients)
            .AddDeveloperSigningCredential();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "ToDoList.Identity.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            return services;
        }
    }
}
