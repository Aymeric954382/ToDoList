using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Duende.IdentityServer.Models;
using Duende.IdentityModel;
using Duende.IdentityServer;
using Microsoft.Extensions.Configuration;

namespace ToDoList.Identity.Infrastructure.Configuration
{
    public static class AuthServerConfiguration
    {
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return configuration.GetSection("IdentityServer:Clients")
                .Get<IEnumerable<Client>>() ?? [];
        }
        public static IEnumerable<ApiScope> GetApiScopes(IConfiguration configuration)
        {
            return configuration.GetSection("IdentityServer:ApiScopes")
                .Get<IEnumerable<ApiScope>>() ?? [];
        }
        public static IEnumerable<ApiResource> GetApiResources(IConfiguration configuration)
        {
            return configuration.GetSection("IdentityServer:ApiResource")
                .Get<IEnumerable<ApiResource>>() ?? [];
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
