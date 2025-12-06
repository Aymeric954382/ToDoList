using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using ToDoList.TaskManager.Application.DI;
using ToDoList.TaskManager.Application.Interfaces;
using ToDoList.TaskManager.Infrastructure.Persistance.DataBaseCommon.EF;
using ToDoList.TaskManager.Infrastructure.Persistance.DI;
using ToDoList.TaskManager.Infrastructure.Persistance.Swagger;
using ToDoList.TaskManager.WebAPI.Middleware;
using ToDoList.TaskManager.WebAPI.Services;


namespace ToDoList.TaskManager.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File("Logs/ToDoListWebAppLog-.txt", rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddAutoMapper(
                Assembly.GetExecutingAssembly(),
                typeof(IToDoDbContext).Assembly
            );

            builder.Services.AddApplication();
            builder.Services.AddPersistance(builder.Configuration);
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("Frontend will be ready soon") //Frontend will be ready soon
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var jwtSettings = builder.Configuration.GetSection("Jwt");
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

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
               ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen();

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

            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

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

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();             

            using (var scope = app.Services.CreateScope())
            {
                var servicesProvider = scope.ServiceProvider;
                try
                {
                    var context = servicesProvider.GetRequiredService<ToDoDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {
                    Log.Fatal(exception, "An error occurred while app initialization");
                }
            }

            app.Run();
        }
    }
}
