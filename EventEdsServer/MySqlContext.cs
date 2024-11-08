using EventEdsServer.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEdsServer;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }
    
    DbSet<StedsEvent> Events { get; set; }
    DbSet<Location> Locations { get; set; }
}