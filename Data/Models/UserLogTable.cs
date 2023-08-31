namespace basicApp.Data.Models;

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public class Notification
{
    [Key]
    public int NotificationId { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Message { get; set; }

    public DateTime Timestamp { get; set; }

    public NotificationStatus Status { get; set; }

    public NotificationPriority Priority { get; set; }

    public string Source { get; set; }

    public string Link { get; set; }

    public string Metadata { get; set; }

    public DateTime? ExpirationDate { get; set; }
}

public enum NotificationStatus
{
    Unread,
    Read,
    Deleted
}

public enum NotificationPriority
{
    Low,
    Medium,
    High
}
