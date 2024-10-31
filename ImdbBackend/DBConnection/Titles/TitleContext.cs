using DataLayer.Titles;
using DataLayer.Ratings;

using Microsoft.EntityFrameworkCore;


namespace DBConnection.Titles
{
    public class TitleContext: DbContext
    {
        public DbSet<Title> Titles { get; set; }

        private readonly string _connectionString;

        public TitleContext(string connectionString) 
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
            modelBuilder.Entity<Title>().ToTable("title_basics_new");
            modelBuilder.Entity<Title>().Property(title => title.TConst).HasColumnName("tconst");
            modelBuilder.Entity<Title>().Property(title => title.TitleType).HasColumnName("titletype");
            modelBuilder.Entity<Title>().Property(title => title.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<Title>().Property(title => title.OriginalTitle).HasColumnName("originaltitle");
            modelBuilder.Entity<Title>().Property(title => title.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<Title>().Property(title => title.StartYear).HasColumnName("startyear");
            modelBuilder.Entity<Title>().Property(title => title.EndYear).HasColumnName("endyear");
            modelBuilder.Entity<Title>().Property(title => title.RuntimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<Title>().Property(title => title.Plot).HasColumnName("plot");
            modelBuilder.Entity<Title>().Property(title => title.Poster).HasColumnName("poster");
            modelBuilder.Entity<Title>().HasOne(title => title.Rating).WithOne(rating=> rating.Title).HasForeignKey<Rating>(rating => rating.TConst);

        }
    }
}
