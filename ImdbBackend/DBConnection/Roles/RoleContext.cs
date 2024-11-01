using DataLayer.Roles;
using Microsoft.EntityFrameworkCore;


namespace DbConnection.Roles;

internal class RoleContext: DbContext
{

    private readonly string _connectionString;

    public DbSet<Role> Roles { get; set; }

    public RoleContext(string connectionString)
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
        modelBuilder.Entity<Role>().ToTable("roles");
        modelBuilder.Entity<Role>().Property(role => role.RoleId).HasColumnName("roleid");
        modelBuilder.Entity<Role>().Property(role => role.RoleName).HasColumnName("name");

    }

}