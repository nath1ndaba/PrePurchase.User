namespace PrePurchase.Mobile.Shared.Models.Notification;

public class RegisterFcmTokenCommand
{
    public string Token { get; set; } = string.Empty;
    public string DeviceType { get; set; } = string.Empty;
}
