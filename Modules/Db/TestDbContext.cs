namespace BasicApp.Modules.Db;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BasicApp.Data.Models;


public class TestDbContext : DbContext
{
    public DbSet<TestUser> TestUsers { get; set; }


    public DbSet<UserNotification> UserNotifications { get; set; }


    public TestDbContext(string ConnectionString) : base(ConnectionString)
    {
        
    }

    public TestDbContext() : base(GetConnectionString())
    {
        
    }

    public static string GetConnectionString()
    {
        var configBuilder = new ConfigurationBuilder()
        .AddJsonFile("config/appsettings.json")
        .AddJsonFile("config/appsettings.Development.json")
        .Build();

#if DEBUG
        string ConnectionString = configBuilder["Database:ConnectionStringTesting"]!;
#else
        string ConnectionString  = configBuilder["Database:ConnectionStringProduction"]!;
#endif
        return ConnectionString;
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

