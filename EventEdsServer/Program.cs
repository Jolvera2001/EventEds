using dotenv.net;
using EventEdsServer.Repository;
using EventEdsServer.Repository.Crud;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

// db context
builder.Services.AddDbContext<MongoContext>(options =>
{
    var mongoUri = Environment.GetEnvironmentVariable("MONGO_URI");
    if (mongoUri != null)
    {
        Console.WriteLine("MongoDB Uri not set!");
        Environment.Exit(0);
    }
    
    var client = new MongoClient(mongoUri);
    var database = client.GetDatabase("main");
    
    options.UseMongoDB(client, database.DatabaseNamespace.DatabaseName);
});

// Add services to the container.
builder.Services.AddControllers();

// crud
builder.Services.AddScoped<IStedsEventCrud, StedsStedsEventCrud>();

// services

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();