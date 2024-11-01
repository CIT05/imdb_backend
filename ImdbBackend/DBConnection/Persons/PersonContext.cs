using DataLayer.Persons;
using Microsoft.EntityFrameworkCore;


namespace DBConnection.Persons
{
    public class PersonContext: DbContext
    {
        public DbSet<Person> Persons { get; set; }

        private readonly string _connectionString;

        public PersonContext(string connectionString) 
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
            modelBuilder.Entity<Person>().ToTable("name_basics_new");
            modelBuilder.Entity<Person>().Property(person => person.NConst).HasColumnName("nconst");
            modelBuilder.Entity<Person>().Property(person => person.primaryName).HasColumnName("primaryname");
            modelBuilder.Entity<Person>().Property(person => person.birthYear).HasColumnName("birthyear");
            modelBuilder.Entity<Person>().Property(person => person.deathYear).HasColumnName("deathyear");

        }
    }
}
