using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.DeadlineIndexer.Application;
using ToDoList.DeadlineIndexer.Infrastructure.Persistance;
using ToDoList.DeadlineIndexer.Infrastructure.Persistance.Mongo;
using ToDoList.DeadlineIndexer.Worker;
using ToDoList.DeadLineIndexer.Contracts;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddContracts(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.Configure<WorkerOptions>(options => builder.Configuration.GetSection("WorkerOptions").Bind(options));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var mongo = scope.ServiceProvider.GetRequiredService<MongoInitializer>();

    mongo.Initialize();
}

host.Run();
