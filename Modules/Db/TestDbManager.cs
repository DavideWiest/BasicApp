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

    public List<Item1Table> GetAll()
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

