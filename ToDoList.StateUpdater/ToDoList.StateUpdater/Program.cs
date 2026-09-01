using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;
using ToDoList.StateUpdater.Application;
using ToDoList.StateUpdater.Infrastructure;
using ToDoList.StateUpdater.Infrastructure.gRPC.Servers;
using ToDoList.StateUpdater.Worker;
using ToDoList.StateUpdater.Worker.Options;

var builder = WebApplication.CreateBuilder(args);

var serilogConfig = new LoggerConfiguration()
    .MinimumLevel
    .Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File(
        "Logs/ToDoListStateUpdater-.txt", 
        shared: true, 
        rollingInterval: RollingInterval.Day)
    .WriteTo.Console(
        LogEventLevel.Information);

Log.Logger = serilogConfig.CreateLogger();

builder.Services.AddSerilog();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

builder.Services.Configure<WorkerOptions>(options => builder.Configuration.GetSection("WorkerOptions"));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.MapGrpcService<GRPCTransferServer>();

app.Run();
