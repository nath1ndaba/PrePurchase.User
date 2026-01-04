using PrePurchase.Mobile.Shared.Models.Auth;
using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.User;
using static PrePurchase.Mobile.Shared.Constants.ApiConstants;

namespace PrePurchase.Mobile.Shared.Services.Authentication;

public interface IAuthenticationService
{
    Task<Result<AuthResponse>> LoginAsync(LoginCommand loginRequest);
    Task<Result<AuthResponse>> RegisterAsync(string firstName, string lastName, string email, string phoneNumber, string password, UserRole role);
    Task<bool> RefreshTokenAsync();
    Task LogoutAsync();
    Task<bool> IsAuthenticatedAsync();
    Task<UserProfileDto?> GetCurrentUserAsync();
}
