namespace PrePurchase.Mobile.Shared.Models.User;

public class ChangePasswordCommand
{
    public Guid UserId { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
}
