using DataLayer.TitleAlternatives;
using Microsoft.EntityFrameworkCore;


namespace DBConnection.TitleAlternativeAlternatives
{
    public class TitleAlternativeContext : DbContext
    { public DbSet<TitleAlternative> TitleAlternatives { get; set; }

    private readonly string _connectionString;

    public TitleAlternativeContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleAlternative>().ToTable("title_akas_new");
        modelBuilder.Entity<TitleAlternative>().HasKey(e => new { e.AkasId, e.Ordering, e.AltTitle, e.Region, e.Language, e.Attributes, e.IsOriginalTitle });
        modelBuilder.Entity<TitleAlternative>().Property(TitleAlternative => TitleAlternative.TitleId).HasColumnName("tconst");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.AkasId).HasColumnName("akasid");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.AltTitle).HasColumnName("title");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Region).HasColumnName("region");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Language).HasColumnName("language");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Attributes).HasColumnName("attributes");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.IsOriginalTitle).HasColumnName("isoriginaltitle");
        
        }
}
}
