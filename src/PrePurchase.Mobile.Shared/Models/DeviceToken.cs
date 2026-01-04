namespace PrePurchase.API.Models;

public class DeviceToken
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty; // "Android" or "iOS"
    public string? DeviceId { get; set; }
    public string? AppVersion { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}