using DataLayer.Genres;
using DataLayer.PersonRoles;
using DataLayer.Persons;
using DataLayer.Ratings;
using DataLayer.Roles;
using DataLayer.Searching;
using DataLayer.TitleAlternatives;
using DataLayer.TitlePrincipals;
using DataLayer.Titles;
using DataLayer.Users;
using DataLayer.Types;
using DataLayer.TitleEpisodes;
using DataLayer.KnownFors;
using Microsoft.EntityFrameworkCore;
using DataLayer.Productions;
using System.Reflection.Emit;
using DataLayer.Bookmarkings;
using DataLayer.History;


namespace DBConnection
{
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

        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<CreateUserResult> CreateUserResults { get; set; }

        public DbSet<UpdateUserResult> UpdateUserResults { get; set; }

        public DbSet<CreateUserResult> CreateUserResult { get; set; }
        public DbSet<UpdateUserResult> UpdateUserResult { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<KnownFor> KnownFors { get; set; }
        public DbSet<Production> Productions { get; set; }

        public DbSet<TitleStringSearchResult> TitleStringSearchResults { get; set; }

        public DbSet<ActorStringSearchResult> ActorStringSearchResults { get; set; }

        public DbSet<RatingForUserResult> RatingForUserResults { get; set; }

        public DbSet<AddRatingResult> AddRatingResults { get; set; }

        public DbSet<PersonsByMovieResult> PersonsByMovieResult { get; set; }

        public DbSet<PersonRatingResult> PersonRatingResults { get; set; }

        public DbSet<ExactSearchResult> ExactSearchResults { get; set; }

        public DbSet<BestSearchResult> BestSearchResults { get; set; }

        public DbSet<TitleBookmarking> TitleBookmarkings { get; set; }
        public DbSet<PersonalityBookmarking> PersonalityBookmarkings { get; set; }

        public DbSet<SearchHistory> SearchHistory { get; set; }

        public DbSet<RatingHistory> RatingHistory { get; set; }


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
            BuildPersonRole(modelBuilder);
            BuildKnownFor(modelBuilder);
            BuildProduction(modelBuilder);
            BuildSearch(modelBuilder);
            BuildTitleBookmarking(modelBuilder);
            BuildPersonalityBookmarking(modelBuilder);
            BuildSearchHistory(modelBuilder);
            BuildRatingHistory(modelBuilder);

        }

        private static void BuildPersons(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("name_basics_new");
            modelBuilder.Entity<Person>().Property(person => person.NConst).HasColumnName("nconst");
            modelBuilder.Entity<Person>().Property(person => person.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<Person>().Property(person => person.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<Person>().Property(person => person.DeathYear).HasColumnName("deathyear");

            modelBuilder.Entity<PersonsByMovieResult>().HasKey(personsBymovieResult => personsBymovieResult.NConst);
            modelBuilder.Entity<PersonsByMovieResult>().Property(e => e.NConst).HasColumnName("nconst");
            modelBuilder.Entity<PersonsByMovieResult>().Property(e => e.PersonRating).HasColumnName("rating");
            modelBuilder.Entity<PersonsByMovieResult>().HasOne(e => e.Person).WithMany().HasForeignKey(e => e.NConst);

        }

        private static void BuildRatings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().ToTable("title_ratings_new");
            modelBuilder.Entity<Rating>().Property(rating => rating.TConst).HasColumnName("tconst").IsRequired();
            modelBuilder.Entity<Rating>().Property(rating => rating.AverageRating).HasColumnName("averagerating");
            modelBuilder.Entity<Rating>().Property(rating => rating.NumberOfVotes).HasColumnName("numvotes");

            modelBuilder.Entity<RatingForUserResult>().HasNoKey();
            modelBuilder.Entity<RatingForUserResult>().Property(e => e.TConst).HasColumnName("tconst");
            modelBuilder.Entity<RatingForUserResult>().Property(e => e.UserId).HasColumnName("userid");
            modelBuilder.Entity<RatingForUserResult>().Property(e => e.TimeStamp).HasColumnName("time_stamp");
            modelBuilder.Entity<RatingForUserResult>().Property(e => e.Rating).HasColumnName("value");

            modelBuilder.Entity<AddRatingResult>().HasNoKey();
            modelBuilder.Entity<AddRatingResult>().Property(e => e.IsSuccess).HasColumnName("add_rating");

            modelBuilder.Entity<PersonRatingResult>().HasNoKey();
            modelBuilder.Entity<PersonRatingResult>().Property(e => e.PersonRating).HasColumnName("get_rating_per_nconst");


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

            modelBuilder.Entity<Title>()
           .HasOne(title => title.Rating)
           .WithOne()
           .HasForeignKey<Rating>(rating => rating.TConst);


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
            modelBuilder.Entity<User>().Property(user => user.Salt).HasColumnName("salt");


            modelBuilder.Entity<CreateUserResult>().HasNoKey();
            modelBuilder.Entity<CreateUserResult>().Property(e => e.UserId).HasColumnName("created_user_result");

            modelBuilder.Entity<UpdateUserResult>().HasNoKey();
            modelBuilder.Entity<UpdateUserResult>().Property(e => e.UserId).HasColumnName("updated_user_result");
        }

        private static void BuildSearch(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleStringSearchResult>().HasNoKey();
            modelBuilder.Entity<TitleStringSearchResult>().Property(stringSearch => stringSearch.TitleId).HasColumnName("idTitle");
            modelBuilder.Entity<TitleStringSearchResult>().Property(stringSearch => stringSearch.Title).HasColumnName("title");

            modelBuilder.Entity<ActorStringSearchResult>().HasNoKey();
            modelBuilder.Entity<ActorStringSearchResult>().Property(stringSearch => stringSearch.ActorId).HasColumnName("actorId");
            modelBuilder.Entity<ActorStringSearchResult>().Property(stringSearch => stringSearch.ActorName).HasColumnName("actorName");

            modelBuilder.Entity<ExactSearchResult>().HasKey(result => result.TConst);
            modelBuilder.Entity<ExactSearchResult>().Property(result => result.TConst).HasColumnName("tconst");
            modelBuilder.Entity<ExactSearchResult>().HasOne(b => b.Title).WithMany().HasForeignKey(b => b.TConst);

            modelBuilder.Entity<BestSearchResult>().HasKey(result => result.TConst);
            modelBuilder.Entity<BestSearchResult>().Property(result => result.TConst).HasColumnName("tconst");
            modelBuilder.Entity<BestSearchResult>().Property(result => result.MatchCount).HasColumnName("match_count");
            modelBuilder.Entity<BestSearchResult>().HasOne(b => b.Title).WithMany().HasForeignKey(b => b.TConst);






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

        private static void BuildTitleBookmarking(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleBookmarking>()
                .ToTable("title_bookmarking") // Mapping to the database table
                .HasKey(tb => new { tb.UserId, tb.TConst }); // Composite key
            modelBuilder.Entity<TitleBookmarking>().Property(tb => tb.TConst).HasColumnName("tconst");
            modelBuilder.Entity<TitleBookmarking>().Property(tb => tb.UserId).HasColumnName("userid");
            modelBuilder.Entity<TitleBookmarking>().Property(tb => tb.Timestamp).HasColumnName("timestamp");


            //modelBuilder.Entity<TitleBookmarking>()
            //    .Property(tb => tb.Timestamp)
            //    .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Set default value for timestamp

            modelBuilder.Entity<TitleBookmarking>()
                .HasOne(tb => tb.User)
                .WithMany(tb => tb.TitleBookmarkings)
                .HasForeignKey(tb => tb.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void BuildPersonalityBookmarking(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalityBookmarking>()
               .ToTable("personality_bookmarking") // Mapping to the database table
               .HasKey(pb => new { pb.UserId, pb.NConst}); // Composite key
            modelBuilder.Entity<PersonalityBookmarking>().Property(pb => pb.NConst).HasColumnName("nconst");
            modelBuilder.Entity<PersonalityBookmarking>().Property(pb => pb.UserId).HasColumnName("userid");
            modelBuilder.Entity<PersonalityBookmarking>().Property(pb => pb.Timestamp).HasColumnName("timestamp");


            //modelBuilder.Entity<TitleBookmarking>()
            //    .Property(tb => tb.Timestamp)
            //    .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Set default value for timestamp

            modelBuilder.Entity<PersonalityBookmarking>()
                .HasOne(pb => pb.User)
                .WithMany(pb => pb.PersonalityBookmarkings)
                .HasForeignKey(pb => pb.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
        private static void BuildSearchHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchHistory>()
                .ToTable("search_history")
                .HasKey(sh => new { sh.UserId, sh.SearchId });
            modelBuilder.Entity<SearchHistory>().Property(sh => sh.SearchId).HasColumnName("searchid");
            modelBuilder.Entity<SearchHistory>().Property(sh => sh.UserId).HasColumnName("userid");
            modelBuilder.Entity<SearchHistory>().Property(sh => sh.Phrase).HasColumnName("phrase");
            modelBuilder.Entity<SearchHistory>().Property(sh => sh.Timestamp).HasColumnName("timestamp");
        }


        private static void BuildRatingHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RatingHistory>()
                .ToTable("rating_history")
                .HasKey(rh => new { rh.UserId, rh.TConst });
            modelBuilder.Entity<RatingHistory>().Property(rh => rh.UserId).HasColumnName("userid");
            modelBuilder.Entity<RatingHistory>().Property(rh => rh.TConst).HasColumnName("tconst");
            modelBuilder.Entity<RatingHistory>().Property(rh => rh.Timestamp).HasColumnName("timestamp");
            modelBuilder.Entity<RatingHistory>().Property(rh => rh.Value).HasColumnName("value");
        }
      
    }
}    
