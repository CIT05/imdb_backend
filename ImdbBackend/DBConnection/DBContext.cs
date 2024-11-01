using DataLayer.Persons;
using DataLayer.Ratings;
using DataLayer.Roles;
using DataLayer.TitleAlternatives;
using DataLayer.TitlePrincipals;
using DataLayer.Titles;
using Microsoft.EntityFrameworkCore;

namespace DBConnection;

    public class ImdbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<TitleAlternative> TitleAlternatives { get; set; }

        public DbSet<TitlePrincipal> TitlePrincipals { get; set; }

    public DbSet<Title> Titles { get; set; }


    private readonly string _connectionString;

        public ImdbContext(string connectionString)
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
          BuildPersons(modelBuilder);
          BuildRatings(modelBuilder);
          BuildRoles(modelBuilder);
          BuildTitleAlternatives(modelBuilder);
          BuildTitlePrincipals(modelBuilder);
          BuildTitle(modelBuilder);

    }

    private static void BuildPersons(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("name_basics_new");
        modelBuilder.Entity<Person>().Property(person => person.NConst).HasColumnName("nconst");
        modelBuilder.Entity<Person>().Property(person => person.primaryName).HasColumnName("primaryname");
        modelBuilder.Entity<Person>().Property(person => person.birthYear).HasColumnName("birthyear");
        modelBuilder.Entity<Person>().Property(person => person.deathYear).HasColumnName("deathyear");
    }

    private static void BuildRatings(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rating>().ToTable("title_ratings_new");
        modelBuilder.Entity<Rating>().Property(rating => rating.TConst).HasColumnName("tconst").IsRequired();
        modelBuilder.Entity<Rating>().Property(rating => rating.AverageRating).HasColumnName("averagerating");
        modelBuilder.Entity<Rating>().Property(rating => rating.NumberOfVotes).HasColumnName("numvotes");
        modelBuilder.Entity<Rating>().HasOne(rating => rating.Title).WithOne(titlte => titlte.Rating).HasForeignKey<Rating>(rating => rating.TConst);
    }

    private static void BuildRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().ToTable("roles");
        modelBuilder.Entity<Role>().Property(role => role.RoleId).HasColumnName("roleid");
        modelBuilder.Entity<Role>().Property(role => role.RoleName).HasColumnName("name");
    }

    private static void BuildTitleAlternatives(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleAlternative>().ToTable("title_akas_new");
        modelBuilder.Entity<TitleAlternative>().HasKey(e => new { e.AkasId});
        modelBuilder.Entity<TitleAlternative>().Property(TitleAlternative => TitleAlternative.TitleId).HasColumnName("tconst");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.AkasId).HasColumnName("akasid");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.AltTitle).HasColumnName("title");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Region).HasColumnName("region");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Language).HasColumnName("language");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Attributes).HasColumnName("attributes");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.IsOriginalTitle).HasColumnName("isoriginaltitle");
    }

    private static void BuildTitlePrincipals(ModelBuilder modelBuilder)
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

    private static void BuildTitle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Title>().ToTable("title_basics_new");
        modelBuilder.Entity<Title>().Property(title => title.TConst).HasColumnName("tconst").IsRequired(); ;
        modelBuilder.Entity<Title>().Property(title => title.TitleType).HasColumnName("titletype");
        modelBuilder.Entity<Title>().Property(title => title.PrimaryTitle).HasColumnName("primarytitle");
        modelBuilder.Entity<Title>().Property(title => title.OriginalTitle).HasColumnName("originaltitle");
        modelBuilder.Entity<Title>().Property(title => title.IsAdult).HasColumnName("isadult");
        modelBuilder.Entity<Title>().Property(title => title.StartYear).HasColumnName("startyear");
        modelBuilder.Entity<Title>().Property(title => title.EndYear).HasColumnName("endyear");
        modelBuilder.Entity<Title>().Property(title => title.RuntimeMinutes).HasColumnName("runtimeminutes");
        modelBuilder.Entity<Title>().Property(title => title.Plot).HasColumnName("plot");
        modelBuilder.Entity<Title>().Property(title => title.Poster).HasColumnName("poster");
        modelBuilder.Entity<Title>().HasOne(title => title.Rating).WithOne(rating => rating.Title).HasForeignKey<Rating>(rating => rating.TConst);
        modelBuilder.Entity<Title>()
            .HasMany(title => title.Principals)
            .WithOne(principal => principal.Title)
            .HasForeignKey(principal => principal.TConst);
    }
    }

