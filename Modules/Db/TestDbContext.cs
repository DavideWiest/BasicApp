namespace BasicApp.Modules.Db;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BasicApp.Data.Models;


public class TestDbContext : DbContext
{
    public DbSet<TestUser> TestUsers { get; set; }


    public DbSet<UserNotification> UserNotifications { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(GetConnectionString());
    }

    private static string GetConnectionString()
    {
#if DEBUG
        return Environment.GetEnvironmentVariable("BasicApp_Database_ConnectionStringDevelopment")!;
#else
        return Environment.GetEnvironmentVariable("BasicApp_Database_ConnectionStringProduction")!;
#endif
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TestUser>().ToTable("TestUser");
        modelBuilder.Entity<UserNotification>().ToTable("UserNotifications");

        // seed content here
        new DbInitializer(modelBuilder).Seed();
    }
}

