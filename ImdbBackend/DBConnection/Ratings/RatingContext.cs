using DataLayer.Ratings;
using DataLayer.Titles;
using Microsoft.EntityFrameworkCore;


namespace DbConnection.Ratings;

internal class RatingContext: DbContext
{

    private readonly string _connectionString;

    public DbSet<Rating> Ratings { get; set; }

    public RatingContext(string connectionString)
    {
        this._connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rating>().ToTable("title_ratings_new");
        modelBuilder.Entity<Rating>().Property(rating => rating.TConst).HasColumnName("tconst").IsRequired();
        modelBuilder.Entity<Rating>().Property(rating => rating.AverageRating).HasColumnName("averagerating");
        modelBuilder.Entity<Rating>().Property(rating => rating.NumberOfVotes).HasColumnName("numvotes");

        // Configuring the one-to-one relationship with Title
        modelBuilder.Entity<Rating>()
            .HasOne<Title>()
            .WithOne(title => title.Rating)
            .HasForeignKey<Rating>(rating => rating.TConst); // TConst in Rating is the FK
    }

}