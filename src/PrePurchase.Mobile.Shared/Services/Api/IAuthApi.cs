using PrePurchase.Mobile.Shared.Models.Auth;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface IAuthApi
{
    [Post("/api/auth/login")]
    Task<Models.Common.ApiResponse<AuthResponse>> LoginAsync([Body] LoginCommand request);

    [Post("/api/auth/register")]
    Task<Models.Common.ApiResponse<AuthResponse>> RegisterAsync([Body] RegisterCommand request);

    [Post("/api/auth/refresh")]
    Task<Models.Common.ApiResponse<AuthResponse>> RefreshTokenAsync([Body] RefreshTokenCommand request);
}
