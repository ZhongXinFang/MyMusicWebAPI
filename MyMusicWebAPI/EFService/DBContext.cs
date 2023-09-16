using Microsoft.EntityFrameworkCore;

namespace MyMusicWebAPI.EFService;

public class DBContext : DbContext
{
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<Language> Language { get; set; }
    public DbSet<Lyric> Lyric { get; set; }
    public DbSet<Song> Song { get; set; }
    public DbSet<User> User { get; set; }

    public DBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.Entity<Artist>();
        modelBuilder.Entity<Country>();
        modelBuilder.Entity<Language>();
        modelBuilder.Entity<Lyric>();
        modelBuilder.Entity<Song>();
        modelBuilder.Entity<User>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("");
    }
}
