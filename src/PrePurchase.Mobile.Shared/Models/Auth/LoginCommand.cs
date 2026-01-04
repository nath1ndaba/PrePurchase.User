namespace PrePurchase.Mobile.Shared.Models.Auth;

public class LoginCommand
{
    public string? EmailOrPhone { get; set; }
    public string? Password { get; set; }
}
