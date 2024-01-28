namespace BasicApp.Modules.Db;

using BasicApp.Data.Models;
using BasicApp.Modules.Logging;


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
            db.UserNotifications.Add(notification);
            db.SaveChanges();
            return true;
        }
        catch (Exception e) { Log.Error(e.ToString()); return false; }
    }

    public List<UserNotification> ReadAll()
    {
        return db.UserNotifications.ToList();
    }

    public List<UserNotification> GetAllByUserId(int UserId)
    {
        return db.UserNotifications.Where(n => n.Id == UserId).ToList();
    }

    public List<UserNotification> GetAllActiveByUserId(int UserId)
    {
        return db.UserNotifications.Where(
            n => n.Id == UserId && n.ExpirationDate < DateTime.Now && n.Status == NotificationStatus.Unread
       ).ToList();
    }
}
