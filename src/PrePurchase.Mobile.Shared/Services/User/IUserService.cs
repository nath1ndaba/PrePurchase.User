using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.User;

namespace PrePurchase.Mobile.Shared.Services.User;

public interface IUserService
{
    Task<Result<UserProfileDto>> GetProfileAsync();
    Task<Result<UserProfileDto>> UpdateProfileAsync(Guid userId, string? firstName = null, string? lastName = null, string? phoneNumber = null);
    Task<Result> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
}
