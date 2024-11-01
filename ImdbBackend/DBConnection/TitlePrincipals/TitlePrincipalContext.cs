using DataLayer.Persons;
using DataLayer.Ratings;
using DataLayer.Roles;
using DataLayer.TitlePrincipals;
using Microsoft.EntityFrameworkCore;


namespace DBConnection.TitlePrincipals
{
    public class TitlePrincipalContext : DbContext
    {   public DbSet<TitlePrincipal> TitlePrincipals { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }

        private readonly string _connectionString;

    public TitlePrincipalContext(string connectionString)
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
        modelBuilder.Entity<TitlePrincipal>().ToTable("title_principals_new");
        modelBuilder.Entity<TitlePrincipal>().HasKey(e => new { e.TConst, e.NConst, e.RoleId, e.Ordering });
        modelBuilder.Entity<TitlePrincipal>().Property(TitlePrincipal => TitlePrincipal.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitlePrincipal>().Property(e => e.NConst).HasColumnName("nconst");
        modelBuilder.Entity<TitlePrincipal>().Property(e => e.RoleId).HasColumnName("roleid");
        modelBuilder.Entity<TitlePrincipal>().Property(e => e.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitlePrincipal>().Property(e => e.Job).HasColumnName("job");
        modelBuilder.Entity<TitlePrincipal>().Property(e => e.Characters).HasColumnName("characters");

        //relationships
        modelBuilder.Entity<TitlePrincipal>()
                  .HasOne(tp => tp.Role)
                  .WithMany(role => role.TitlePrincipals)
                  .HasForeignKey(tp => tp.RoleId);
        modelBuilder.Entity<TitlePrincipal>()
               .HasOne(tp => tp.Person)
               .WithMany(person => person.TitlePrincipals)
               .HasForeignKey(tp => tp.NConst);

        modelBuilder.Entity<TitlePrincipal>()
         .HasOne(tp => tp.Title) // Each TitlePrincipal has one Title
         .WithMany(title => title.Principals) // Title has many TitlePrincipals
         .HasForeignKey(tp => tp.TConst); // Foreign key in TitlePrincipal is TConst

        }
}
}
