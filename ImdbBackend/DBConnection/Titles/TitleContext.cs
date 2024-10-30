using DataLayer.Titles;
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
            modelBuilder.Entity<Title>().Property(title => title.titleType).HasColumnName("titletype");
            modelBuilder.Entity<Title>().Property(title => title.primaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<Title>().Property(title => title.originalTitle).HasColumnName("originaltitle");
            modelBuilder.Entity<Title>().Property(title => title.isAdult).HasColumnName("isadult");
            modelBuilder.Entity<Title>().Property(title => title.startYear).HasColumnName("startyear");
            modelBuilder.Entity<Title>().Property(title => title.endYear).HasColumnName("endyear");
            modelBuilder.Entity<Title>().Property(title => title.runtimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<Title>().Property(title => title.plot).HasColumnName("plot");
            modelBuilder.Entity<Title>().Property(title => title.poster).HasColumnName("poster");

        }
    }
}
