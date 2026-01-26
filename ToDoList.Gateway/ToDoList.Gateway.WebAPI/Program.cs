using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace ToDoList.Gateway.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string redisConnection = builder.Configuration.GetConnectionString("Redis");

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(redisConnection);
            });

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            var jwtSettings = builder.Configuration.GetSection("UserJwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();

            app.Run();
        }
    }
}
