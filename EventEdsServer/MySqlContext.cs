using EventEdsServer.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEdsServer;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }
    
    public DbSet<StedsEvent> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
}