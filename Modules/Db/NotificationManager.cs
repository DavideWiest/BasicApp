using BasicApp.Data.Models;

namespace BasicApp.Modules.Db;


public class NotificationManager
{

    private TestDbContext db;

    public NotificationManager(TestDbContext db)
    {
        this.db = db;
    }

    public bool InsertOne(UserNotification notification)
    {
        try
        {
            db.NotificationTable.Add(notification);
            db.SaveChanges();
            return true;
        }
        catch { return false; }
    }

    public List<UserNotification> ReadAll()
    {
        return db.NotificationTable.ToList();
    }
}
