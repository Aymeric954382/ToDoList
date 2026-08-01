using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using ToDoList.Gateway.Application.Common.Mappings.Helpers;
using ToDoList.Gateway.Application.Common.Mappings.Profiles;
using ToDoList.Gateway.Application.Interfaces;
using ToDoList.Gateway.Contracts.Helpers;
using ToDoList.Gateway.Infrastructure.Persistance.DI;
using ToDoList.Gateway.Infrastructure.Persistance.Rabbit;
using ToDoList.Gateway.Infrastructure.Persistance.Swagger;
using ToDoList.Gateway.WebAPI.Services;
using ToDoList.TaskStateService.WebAPI.Middlewares;

namespace ToDoList.Gateway.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMarkerContracts).Assembly));
                cfg.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMarkerApplication).Assembly));
            });
            
            //Logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File("Logs/ToDoListWebAppLog-.txt", rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();

            //Add Infrastructure
            builder.Services.AddPersistance(builder.Configuration);

            //Auth
            builder.Services.AddAuthorization();

            //Controllers
            builder.Services.AddControllers();

            //Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("Frontend will be ready soon") //Frontend will be ready soon
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
                  
            //For get user 
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
            builder.Services.AddHttpContextAccessor();

            //Versioning
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

            //JWT 
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

            //Swagger
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
                ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    config.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                config.RoutePrefix = string.Empty;

                config.ConfigObject.AdditionalItems["cacheBuster"] = true;
            });

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var rabbitInitializer = serviceProvider.GetRequiredService<RabbitInitializer>();

                    await rabbitInitializer.InitializeAsync();
                }
                catch(Exception ex)
                {
                    Log.Fatal(ex, "An error occurred while app initialization");

                    throw;
                }
            }

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
