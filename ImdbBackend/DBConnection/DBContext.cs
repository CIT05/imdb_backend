using DataLayer.Genres;
using DataLayer.Persons;
using DataLayer.Ratings;
using DataLayer.Roles;
using DataLayer.TitleAlternatives;
using DataLayer.TitlePrincipals;
using DataLayer.Titles;
using DataLayer.Users;
using DataLayer.Types;
using DataLayer.TitleEpisodes;
using Microsoft.EntityFrameworkCore;

namespace DBConnection;

    public class ImdbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<TitleAlternative> TitleAlternatives { get; set; }

        public DbSet<TitlePrincipal> TitlePrincipals { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<TitleType> Types { get; set; }


        public DbSet<TitleEpisode> TitleEpisodes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CreateUserResult> CreateUserResult { get; set; }

        public DbSet<UpdateUserResult> UpdateUserResult { get; set; }

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
          BuildGenres(modelBuilder);
          BuildTypes(modelBuilder);
          BuildTitle(modelBuilder);
          BuildTitleEpisodes(modelBuilder);
          BuildUser(modelBuilder);


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
        modelBuilder.Entity<TitleAlternative>().HasKey(e => new { e.AkasId});
        modelBuilder.Entity<TitleAlternative>().Property(TitleAlternative => TitleAlternative.TitleId).HasColumnName("tconst");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.AkasId).HasColumnName("akasid");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.AltTitle).HasColumnName("title");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Region).HasColumnName("region");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Language).HasColumnName("language");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.Attributes).HasColumnName("attributes");
        modelBuilder.Entity<TitleAlternative>().Property(e => e.IsOriginalTitle).HasColumnName("isoriginaltitle");

        //relationships
        modelBuilder.Entity<TitleAlternative>()
         .HasOne(tA => tA.Title) 
         .WithMany(title => title.TitleAlternatives) 
         .HasForeignKey(tp => tp.TitleId);


        modelBuilder.Entity<TitleAlternative>()
        .HasMany(title => title.Types)
        .WithMany(types => types.Titles)
        .UsingEntity<Dictionary<string, object>>(
            "title_types",
        j => j.HasOne<TitleType>()
              .WithMany()
              .HasForeignKey("typeid")  
            .HasConstraintName("title_types_typeid_fkey"),

        j => j.HasOne<TitleAlternative>()
              .WithMany()
              .HasForeignKey("akasid")
              .HasConstraintName("title_types_akasid_fkey")
    );
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
        modelBuilder.Entity<Title>().HasOne(title => title.Rating).WithOne().HasForeignKey<Rating>(rating => rating.TConst);
        modelBuilder.Entity<Title>()
            .HasMany(title => title.Principals)
            .WithOne(principal => principal.Title)
            .HasForeignKey(principal => principal.TConst);
        modelBuilder.Entity<Title>()
          .HasMany(title => title.TitleAlternatives)
            .WithOne(alt => alt.Title)
            .HasForeignKey(ta => ta.TitleId);

     modelBuilder.Entity<Title>()
    .HasMany(title => title.Genres)
    .WithMany(genre => genre.Titles)
    .UsingEntity<Dictionary<string, object>>(
        "title_genres",
        j => j.HasOne<Genre>()
              .WithMany()
              .HasForeignKey("genreid")   // Specify FK column name for Genre
              .HasConstraintName("title_genres_genreid_fkey"),
        j => j.HasOne<Title>()
              .WithMany()
              .HasForeignKey("tconst")  // Specify FK column name for Title
              .HasConstraintName("title_genres_tconst_fkey")
    );
    modelBuilder.Entity<Title>()
    .HasMany(title => title.Episodes)
    .WithOne(ep => ep.Title)
    .HasForeignKey(ep => ep.ParentTConst);

   
  }

        private static void BuildGenres(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().ToTable("genres");
            modelBuilder.Entity<Genre>().Property(genre => genre.GenreId).HasColumnName("genreid");
            modelBuilder.Entity<Genre>().Property(genre => genre.GenreName).HasColumnName("name");
        }


        private static void BuildTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleType>().ToTable("types");
            modelBuilder.Entity<TitleType>().Property(type => type.TypeId).HasColumnName("typeid");
            modelBuilder.Entity<TitleType>().Property(type => type.TypeName).HasColumnName("type");
        }



    private static void BuildTitleEpisodes(ModelBuilder modelBuilder)
    {
     modelBuilder.Entity<TitleEpisode>().ToTable("title_episode_new");
        modelBuilder.Entity<TitleEpisode>().Property(e => e.Tconst).HasColumnName("tconst");
        modelBuilder.Entity<TitleEpisode>().Property(e => e.ParentTConst).HasColumnName("parenttconst");
        modelBuilder.Entity<TitleEpisode>().Property(e => e.SeasonNumber).HasColumnName("seasonnumber");
        modelBuilder.Entity<TitleEpisode>().Property(e => e.EpisodeNumber).HasColumnName("episodenumber");
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

}

