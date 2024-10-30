using dotenv.net;
using EventEdsServer.Repository;
using EventEdsServer.Repository.Crud;
using EventEdsServer.Services;
using EventEdsServer.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

// db context
builder.Services.AddDbContext<MongoContext>(options =>
{
    var mongoUri = Environment.GetEnvironmentVariable("MONGO_URI");
    if (mongoUri == null)
    {
        Console.WriteLine("MongoDB Uri not set!");
        Environment.Exit(0);
    }
    
    var mongoUrl = new MongoUrl(mongoUri);
    var client = new MongoClient(mongoUri);
    var database = client.GetDatabase(mongoUrl.DatabaseName ?? "main");
    
    options.UseMongoDB(client, database.DatabaseNamespace.DatabaseName);
});

// CORS for local development
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueDevelopment", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();

// crud
builder.Services.AddScoped<IStedsEventCrud, StedsStedsEventCrud>();

// services
builder.Services.AddScoped<IStedsEventService, StedsEventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("VueDevelopment");
app.UseAuthorization();
app.MapControllers();
app.Run();