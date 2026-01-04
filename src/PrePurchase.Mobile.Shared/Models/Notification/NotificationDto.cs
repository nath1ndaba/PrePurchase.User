namespace PrePurchase.Mobile.Shared.Models.Notification;

public class NotificationDto
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public object? Data { get; set; }
}
public class RealtimeNotificationDto
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public Dictionary<string, object>? Data { get; set; }
}
