using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;
using ToDoList.DeadlineIndexer.Application;
using ToDoList.DeadlineIndexer.Infrastructure.Persistance;
using ToDoList.DeadlineIndexer.Infrastructure.Persistance.Mongo;
using ToDoList.DeadlineIndexer.Worker;
using ToDoList.DeadLineIndexer.Contracts;

var builder = Host.CreateApplicationBuilder(args);

var loggerConfig = new LoggerConfiguration()
    .MinimumLevel
    .Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File(
        "Logs/ToDoListWorkerIndexer-.txt", 
        shared: true,
        rollingInterval: RollingInterval.Day)
    .WriteTo.Console(
        restrictedToMinimumLevel: LogEventLevel.Information);

Log.Logger = loggerConfig.CreateLogger();

builder.Services.AddSerilog();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

builder.Services.AddContracts(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var mongo = scope.ServiceProvider.GetRequiredService<MongoInitializer>();

    mongo.Initialize();
}

host.Run();

