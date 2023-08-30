namespace Modules.Db;

using Data.Db;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


public class TestDbManager
{

    private TestDbContext db;

    public TestDbManager(TestDbContext db)
    {
        this.db = db;
    }

    public bool InsertOne(string Name, string Job, short Age)
    {
        try
        {
            Item1Table newPerson = new Item1Table {Name=Name, Job=Job, Age=Age };
            db.Item1Table.Add(newPerson);
            db.SaveChanges();
            return true;
        } catch { return false; }
    }

    public List<Item1Table> ReadAll()
    {
        return db.Item1Table.ToList();
    }
}

public class TestDbContext : DbContext
{
    public DbSet<Item1Table> Item1Table { get; set; }

    public TestDbContext(string ConnectionString) : base(ConnectionString)
    {
        
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        base.OnModelCreating(modelBuilder);
        // seed content here
        new DbInitializer(modelBuilder).Seed();
    }
}

