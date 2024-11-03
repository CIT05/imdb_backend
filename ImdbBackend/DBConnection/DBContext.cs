using DataLayer.PersonRoles;
using DataLayer.Persons;
using DataLayer.Ratings;
using DataLayer.Roles;
using DataLayer.TitleAlternatives;
using DataLayer.TitlePrincipals;
using DataLayer.Titles;
using DataLayer.Users;
using DataLayer.KnownFors;
using Microsoft.EntityFrameworkCore;
using DataLayer.Productions;

namespace DBConnection
{
    public class ImdbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TitleAlternative> TitleAlternatives { get; set; }
        public DbSet<TitlePrincipal> TitlePrincipals { get; set; }
        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CreateUserResult> CreateUserResult { get; set; }
        public DbSet<UpdateUserResult> UpdateUserResult { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<KnownFor> KnownFors { get; set; }
        public DbSet<Production> Productions { get; set; }


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
            BuildPersonRole(modelBuilder);
            BuildUser(modelBuilder);
            BuildKnownFor(modelBuilder);
            BuildProduction(modelBuilder);
        }

        private static void BuildPersons(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("name_basics_new");
            modelBuilder.Entity<Person>().Property(person => person.NConst).HasColumnName("nconst");
            modelBuilder.Entity<Person>().Property(person => person.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<Person>().Property(person => person.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<Person>().Property(person => person.DeathYear).HasColumnName("deathyear");
        }

        private static void BuildRatings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().ToTable("title_ratings_new");
            modelBuilder.Entity<Rating>().Property(rating => rating.TConst).HasColumnName("tconst").IsRequired();
            modelBuilder.Entity<Rating>().Property(rating => rating.AverageRating).HasColumnName("averagerating");
            modelBuilder.Entity<Rating>().Property(rating => rating.NumberOfVotes).HasColumnName("numvotes");
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
            modelBuilder.Entity<TitleAlternative>().HasKey(e => new { e.AkasId });
            modelBuilder.Entity<TitleAlternative>().Property(e => e.TitleId).HasColumnName("tconst");
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
            modelBuilder.Entity<TitlePrincipal>().Property(e => e.TConst).HasColumnName("tconst");
            modelBuilder.Entity<TitlePrincipal>().Property(e => e.NConst).HasColumnName("nconst");
            modelBuilder.Entity<TitlePrincipal>().Property(e => e.RoleId).HasColumnName("roleid");
            modelBuilder.Entity<TitlePrincipal>().Property(e => e.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitlePrincipal>().Property(e => e.Job).HasColumnName("job");
            modelBuilder.Entity<TitlePrincipal>().Property(e => e.Characters).HasColumnName("characters");

            // Relationships
            modelBuilder.Entity<TitlePrincipal>()
                .HasOne(tp => tp.Role)
                .WithMany(role => role.TitlePrincipals)
                .HasForeignKey(tp => tp.RoleId);

            modelBuilder.Entity<TitlePrincipal>()
                .HasOne(tp => tp.Person)
                .WithMany(person => person.TitlePrincipals)
                .HasForeignKey(tp => tp.NConst);

            modelBuilder.Entity<TitlePrincipal>()
                .HasOne(tp => tp.Title)
                .WithMany(title => title.Principals)
                .HasForeignKey(tp => tp.TConst);
        }

        private static void BuildTitle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>().ToTable("title_basics_new");
            modelBuilder.Entity<Title>().Property(title => title.TConst).HasColumnName("tconst").IsRequired();
            modelBuilder.Entity<Title>().Property(title => title.TitleType).HasColumnName("titletype");
            modelBuilder.Entity<Title>().Property(title => title.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<Title>().Property(title => title.OriginalTitle).HasColumnName("originaltitle");
            modelBuilder.Entity<Title>().Property(title => title.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<Title>().Property(title => title.StartYear).HasColumnName("startyear");
            modelBuilder.Entity<Title>().Property(title => title.EndYear).HasColumnName("endyear");
            modelBuilder.Entity<Title>().Property(title => title.RuntimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<Title>().Property(title => title.Plot).HasColumnName("plot");
            modelBuilder.Entity<Title>().Property(title => title.Poster).HasColumnName("poster");
            modelBuilder.Entity<Title>()
                .HasOne(title => title.Rating)
                .WithOne()
                .HasForeignKey<Rating>(rating => rating.TConst);
            modelBuilder.Entity<Title>()
                .HasMany(title => title.Principals)
                .WithOne(principal => principal.Title)
                .HasForeignKey(principal => principal.TConst);
        }

        private static void BuildPersonRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonRole>().ToTable("name_role");
            modelBuilder.Entity<PersonRole>().HasKey(e => new { e.NConst, e.RoleId });
            modelBuilder.Entity<PersonRole>().Property(e => e.NConst).HasColumnName("nconst").IsRequired();
            modelBuilder.Entity<PersonRole>().Property(e => e.RoleId).HasColumnName("roleid").IsRequired();
            modelBuilder.Entity<PersonRole>().Property(e => e.Ordering).HasColumnName("ordering");

            modelBuilder.Entity<PersonRole>()
                .HasOne(pr => pr.Role)
                .WithMany(role => role.PersonRoles)
                .HasForeignKey(pr => pr.RoleId);
            modelBuilder.Entity<PersonRole>()
                .HasOne(pr => pr.Person)
                .WithMany(person => person.PersonRoles)
                .HasForeignKey(pr => pr.NConst);
        }

        private static void BuildUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(user => user.UserId).HasColumnName("userid");
            modelBuilder.Entity<User>().Property(user => user.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(user => user.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(user => user.Language).HasColumnName("language");

            modelBuilder.Entity<CreateUserResult>().HasNoKey();
            modelBuilder.Entity<CreateUserResult>().Property(e => e.UserId).HasColumnName("created_user_result");

            modelBuilder.Entity<UpdateUserResult>().HasNoKey();
            modelBuilder.Entity<UpdateUserResult>().Property(e => e.UserId).HasColumnName("updated_user_result");
        }

        private static void BuildKnownFor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KnownFor>().ToTable("name_title");
            modelBuilder.Entity<KnownFor>().HasKey(e => new { e.NConst, e.TConst });
            modelBuilder.Entity<KnownFor>().Property(knownfor => knownfor.NConst).HasColumnName("nconst");
            modelBuilder.Entity<KnownFor>().Property(knownfor => knownfor.TConst).HasColumnName("tconst");
            modelBuilder.Entity<KnownFor>().Property(knownfor => knownfor.Ordering).HasColumnName("ordering");

            modelBuilder.Entity<KnownFor>()
                    .HasOne(title => title.Title)
                    .WithMany(kf => kf.KnownFors)
                    .HasForeignKey(pr => pr.TConst);

            modelBuilder.Entity<KnownFor>()
                .HasOne(pr => pr.Person)
                .WithMany(kf => kf.KnownFors)
                .HasForeignKey(pr => pr.NConst);
        }

        private static void BuildProduction(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Production>().ToTable("title_crew_new");
            modelBuilder.Entity<Production>().HasKey(e => new { e.NConst, e.TConst, e.RoleId });
            modelBuilder.Entity<Production>().Property(production => production.NConst).HasColumnName("nconst");
            modelBuilder.Entity<Production>().Property(production => production.TConst).HasColumnName("tconst");
            modelBuilder.Entity<Production>().Property(production => production.RoleId).HasColumnName("roleid");

            modelBuilder.Entity<Production>()
                    .HasOne(title => title.Title)
                    .WithMany(production => production.ProductionPersons)
                    .HasForeignKey(pr => pr.TConst);

            modelBuilder.Entity<Production>()
                .HasOne(pr => pr.Person)
                .WithMany(production => production.ProductionPersons)
                .HasForeignKey(pr => pr.NConst);
            modelBuilder.Entity<Production>()
              .HasOne(r => r.Role)
              .WithMany(production => production.ProductionPersons)
              .HasForeignKey(pr => pr.RoleId);
        }
    }
}
