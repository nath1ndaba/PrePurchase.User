using static PrePurchase.Mobile.Shared.Constants.ApiConstants;

namespace PrePurchase.Mobile.Shared.Models.Auth;

public class RegisterCommand
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public UserRole Role { get; set; }  // UserRole enum as int
}
