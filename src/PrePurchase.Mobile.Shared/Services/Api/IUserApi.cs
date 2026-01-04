using PrePurchase.Mobile.Shared.Models.User;
using PrePurchase.Mobile.Shared.Models.Common;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface IUserApi
{
    [Get("/api/users/me")]
    Task<Models.Common.ApiResponse<UserProfileDto>> GetProfileAsync();

    [Put("/api/users/me")]
    Task<Models.Common.ApiResponse<UserProfileDto>> UpdateProfileAsync([Body] UpdateUserProfileCommand request);

    [Post("/api/users/change-password")]
    Task<Models.Common.ApiResponse<UserProfileDto>> ChangePasswordAsync([Body] ChangePasswordCommand request);
}
