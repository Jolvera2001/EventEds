using EventEdsServer.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace EventEdsServer.Repository;

public class MongoContext : DbContext
{
    public DbSet<StedsEvent> Events { get; set; }
    
    public static MongoContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<MongoContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
    
    public MongoContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<StedsEvent>().ToCollection("Events");
        
    }
}