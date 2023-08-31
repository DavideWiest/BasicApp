namespace BasicApp.Modules.Db;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BasicApp.Data.Models;

public class UserManager
{

    private TestDbContext db = default!;

    public UserManager(TestDbContext db)
    {
        this.db = db;
    }

    public bool InsertOne(string Name, string Job, short Age)
    {
        try
        {
            TestUser newPerson = new TestUser {Name=Name, Job=Job, Age=Age };
            db.TestUsers.Add(newPerson);
            db.SaveChanges();
            return true;
        } catch { return false; }
    }

    public List<TestUser> ReadAll()
    {
        return db.TestUsers.ToList();
    }
}

public class TestDbContext : DbContext
{
    public DbSet<TestUser> TestUsers { get; set; }


    public DbSet<UserNotification> UserNotifications { get; set; }


    public TestDbContext(string ConnectionString) : base(ConnectionString)
    {
        
    }

    public TestDbContext() : base("postgres://postgres:00069@localhost/testdb2")
    {
        
    }

    public static string GetConnectionString()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Configuration.AddJsonFile("config/appsettings.json");
        builder.Configuration.AddJsonFile("config/appsettings.Development.json");
        #if DEBUG
        string ConnectionString = builder.Configuration["Database:ConnectionStringTesting"]!;
#else
        string ConnectionString  = builder.Configuration["Database:ConnectionStringProduction"]!;
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

