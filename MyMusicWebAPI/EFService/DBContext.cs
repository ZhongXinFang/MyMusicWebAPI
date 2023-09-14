using Microsoft.EntityFrameworkCore;

namespace MyMusicWebAPI.EFService;

public class DBContext : DbContext
{
    public DbSet<User> User { get; set; }

    public DBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>();
    }
}
