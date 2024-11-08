using dotenv.net;
using EventEdsServer;
using EventEdsServer.Services;
using EventEdsServer.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");

builder.Services.AddDbContext<MySqlContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

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

// services
builder.Services.AddScoped<IStedsEventService, StedsEventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("VueDevelopment");
app.UseAuthorization();
app.MapControllers();
app.Run();